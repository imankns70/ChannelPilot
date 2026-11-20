using ChannelPilot.Api.Contracts.ContentSources;
using ChannelPilot.Application.ContentSources;
using ChannelPilot.Domain.Content;
using Microsoft.AspNetCore.Mvc;

namespace ChannelPilot.Api.Controllers;

[ApiController]
[Route("api/content-sources")]
public sealed class ContentSourcesController(
    ContentSourceManagementService contentSourceManagementService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ContentSourceResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var sources = await contentSourceManagementService.GetAllAsync(cancellationToken);
        return Ok(sources.Select(ToResponse).ToList());
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ContentSourceResponse>> GetById(
        long id,
        CancellationToken cancellationToken)
    {
        var source = await contentSourceManagementService.GetByIdAsync(id, cancellationToken);
        return source is null ? NotFound() : Ok(ToResponse(source));
    }

    [HttpPost]
    public async Task<ActionResult<ContentSourceResponse>> Create(
        CreateContentSourceRequest request,
        CancellationToken cancellationToken)
    {
        var source = await contentSourceManagementService.CreateAsync(
            new ContentSource
            {
                Name = request.Name,
                BaseUrl = request.BaseUrl,
                SourceKind = request.SourceKind,
                IsActive = request.IsActive,
                ReliabilityScore = request.ReliabilityScore,
                LastFetchedAt = request.LastFetchedAt,
                FailureCount = request.FailureCount
            },
            cancellationToken);

        var response = ToResponse(source);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ContentSourceResponse>> Update(
        long id,
        UpdateContentSourceRequest request,
        CancellationToken cancellationToken)
    {
        var source = await contentSourceManagementService.UpdateAsync(
            id,
            new ContentSource
            {
                Name = request.Name,
                BaseUrl = request.BaseUrl,
                SourceKind = request.SourceKind,
                IsActive = request.IsActive,
                ReliabilityScore = request.ReliabilityScore,
                LastFetchedAt = request.LastFetchedAt,
                FailureCount = request.FailureCount
            },
            cancellationToken);

        return source is null ? NotFound() : Ok(ToResponse(source));
    }

    [HttpPost("{id:long}/activate")]
    public async Task<ActionResult<ContentSourceResponse>> Activate(
        long id,
        CancellationToken cancellationToken)
    {
        var source = await contentSourceManagementService.ActivateAsync(id, cancellationToken);
        return source is null ? NotFound() : Ok(ToResponse(source));
    }

    [HttpPost("{id:long}/disable")]
    public async Task<ActionResult<ContentSourceResponse>> Disable(
        long id,
        CancellationToken cancellationToken)
    {
        var source = await contentSourceManagementService.DisableAsync(id, cancellationToken);
        return source is null ? NotFound() : Ok(ToResponse(source));
    }

    private static ContentSourceResponse ToResponse(ContentSource source) => new(
        source.Id,
        source.Name,
        source.BaseUrl,
        source.SourceKind,
        source.IsActive,
        source.ReliabilityScore,
        source.LastFetchedAt,
        source.FailureCount,
        source.CreatedAt,
        source.UpdatedAt);
}
