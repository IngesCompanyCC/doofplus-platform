using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

/// <summary>
///     REST controller of the monthly trend of deviations by root cause.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Deviation Trends")]
public class DeviationTrendsController(IDeviationTrendQueryService deviationTrendQueryService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get the deviation trend")]
    [EndpointDescription("Counts the deviations by month and root cause, oldest month first; id is the position in the list.")]
    [ProducesResponseType(typeof(IEnumerable<DeviationTrendResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDeviationTrends(CancellationToken cancellationToken)
    {
        var trends = await deviationTrendQueryService.Handle(new GetDeviationTrendsQuery(), cancellationToken);
        return Ok(trends.Select((trend, index) => new DeviationTrendResource(index + 1, trend.RootCause, trend.Month, trend.Count)));
    }
}
