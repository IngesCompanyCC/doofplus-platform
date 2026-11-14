using IngesCompanyCCPlatform.API.Monitoring.Domain.Model;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using IngesCompanyCCPlatform.API.Monitoring.Application.CommandServices;
using IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Monitoring.Resources;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest;

/// <summary>
///     Endpoint that receives the telemetry sent by ThingsBoard (TS04).
/// </summary>
/// <remarks>
///     ThingsBoard sends the API key in the <c>X-Api-Key</c> header. It is compared with
///     <c>ThingsBoard:ApiKey</c> of the configuration (in production, the environment variable
///     <c>ThingsBoard__ApiKey</c>). Without a configured key every request is rejected.
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Telemetry")]
public class TelemetryController(
    IReadingCommandService readingCommandService,
    IConfiguration configuration,
    IStringLocalizer<MonitoringMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    /// <summary>
    ///     The header where ThingsBoard sends its API key.
    /// </summary>
    public const string ApiKeyHeader = "X-Api-Key";

    private const string ApiKeyInvalidMessageKey = "monitoring.telemetry.error.apiKey.invalid";

    [HttpPost]
    [EndpointSummary("Receive a reading from ThingsBoard")]
    [EndpointDescription("Stores the reading of the sensor linked to the device and answers 202 Accepted; without a valid API key in X-Api-Key answers 401 and discards it (TS04).")]
    [ProducesResponseType(typeof(ReadingResource), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReceiveTelemetry([FromHeader(Name = ApiKeyHeader)] string? apiKey,
        [FromBody] TelemetryResource resource, CancellationToken cancellationToken)
    {
        if (!IsValidApiKey(apiKey))
            return problemDetailsFactory.CreateProblemDetails(this, StatusCodes.Status401Unauthorized, MonitoringError.InvalidApiKey,
                MonitoringActionResultAssembler.Localize(messages, ApiKeyInvalidMessageKey,
                    "The API key of ThingsBoard is missing or not valid."));

        var command = ReadingCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await readingCommandService.Handle(command, cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromCommandResult(this, result, messages,
            problemDetailsFactory,
            reading => Accepted(ReadingResourceFromEntityAssembler.ToResourceFromEntity(reading)));
    }

    private bool IsValidApiKey(string? apiKey)
    {
        var expected = configuration["ThingsBoard:ApiKey"];
        if (string.IsNullOrEmpty(expected) || string.IsNullOrEmpty(apiKey)) return false;
        return CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(apiKey), Encoding.UTF8.GetBytes(expected));
    }
}
