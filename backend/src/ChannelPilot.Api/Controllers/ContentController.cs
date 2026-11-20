using ChannelPilot.Api.Contracts.Content;
using ChannelPilot.Domain.Content;
using Microsoft.AspNetCore.Mvc;

namespace ChannelPilot.Api.Controllers;

[ApiController]
[Route("api/content")]
public sealed class ContentController : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<ContentResponse>> GetAll()
    {
        return Ok(new[] { CreatePlaceholder(1) });
    }

    [HttpGet("{id:long}")]
    public ActionResult<ContentResponse> GetById(long id)
    {
        return Ok(CreatePlaceholder(id));
    }

    [HttpPost("{id:long}/analyze")]
    public ActionResult<ContentResponse> Analyze(long id, AnalyzeContentRequest request)
    {
        return Ok(CreatePlaceholder(id) with
        {
            Status = ContentStatus.Processed,
            FinalScore = 0
        });
    }

    [HttpPost("{id:long}/rewrite")]
    public ActionResult<ContentResponse> Rewrite(long id, RewriteContentRequest request)
    {
        return Ok(CreatePlaceholder(id) with
        {
            OriginalTitle = $"Placeholder rewrite for {request.ChannelTone}"
        });
    }

    private static ContentResponse CreatePlaceholder(long id) => new(
        id,
        ContentType.FootballNews,
        ContentStatus.Collected,
        "Placeholder content",
        "No content processing has been executed.",
        null,
        null,
        "placeholder",
        DateTime.UnixEpoch,
        null);
}
