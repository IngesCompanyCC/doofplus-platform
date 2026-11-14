using System.Net.Mime;
using IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest;

[ApiController]
[Route("api/v1/contact-inquiries")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Contact Inquiries")]
public class ContactInquiriesController(
    IContactInquiryCommandService commandService,
    IContactInquiryQueryService queryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Send contact inquiry")]
    [EndpointDescription("Registers a new contact inquiry submitted from the public Contact us form.")]
    [ProducesResponseType(typeof(ContactInquiryResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendContactInquiry(
        [FromBody] SendContactInquiryResource resource,
        CancellationToken cancellationToken)
    {
        var command = SendContactInquiryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);

        return OrganizationsActionResultAssembler.ToActionResult(
            this,
            result,
            problemDetailsFactory,
            inquiry => CreatedAtAction(
                nameof(GetContactInquiryById),
                new { inquiryId = inquiry.Id },
                ContactInquiryResourceFromEntityAssembler.ToResourceFromEntity(inquiry)));
    }

    [HttpGet("{inquiryId:int}")]
    [EndpointSummary("Get contact inquiry by id")]
    [EndpointDescription("Retrieves a single contact inquiry by its identifier.")]
    [ProducesResponseType(typeof(ContactInquiryResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContactInquiryById(
        int inquiryId,
        CancellationToken cancellationToken)
    {
        var query = new GetContactInquiryByIdQuery(inquiryId);
        var inquiry = await queryService.Handle(query, cancellationToken);

        if (inquiry is null)
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status404NotFound,
                OrganizationsError.ContactInquiryNotFound,
                $"Contact inquiry with ID {inquiryId} was not found.");

        return Ok(ContactInquiryResourceFromEntityAssembler.ToResourceFromEntity(inquiry));
    }

    [HttpGet]
    [EndpointSummary("Get all contact inquiries")]
    [EndpointDescription("Retrieves all contact inquiries registered in the system.")]
    [ProducesResponseType(typeof(IEnumerable<ContactInquiryResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllContactInquiries(CancellationToken cancellationToken)
    {
        var inquiries = await queryService.Handle(new GetAllContactInquiriesQuery(), cancellationToken);
        var resources = inquiries.Select(ContactInquiryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}