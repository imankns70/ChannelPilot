using ChannelPilot.Api.Contracts.Novels;
using ChannelPilot.Application.Novels;
using ChannelPilot.Domain.Novels;
using Microsoft.AspNetCore.Mvc;

namespace ChannelPilot.Api.Controllers;

[ApiController]
[Route("api/novels")]
public sealed class NovelsController(NovelEpisodeSplitter episodeSplitter) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<NovelResponse>> GetAll()
    {
        return Ok(new[] { CreatePlaceholder(1) });
    }

    [HttpGet("{id:long}")]
    public ActionResult<NovelResponse> GetById(long id)
    {
        return Ok(CreatePlaceholder(id));
    }

    [HttpPost]
    public ActionResult<NovelResponse> Create(CreateNovelRequest request)
    {
        var response = new NovelResponse(
            1,
            request.Title,
            request.Author,
            request.Description,
            request.Genre,
            NovelStatus.Draft,
            0);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPost("import")]
    public ActionResult<IReadOnlyList<NovelEpisodeResponse>> Import(ImportNovelRequest request)
    {
        var episodes = episodeSplitter.Split(request.Text)
            .Select(draft => new NovelEpisodeResponse(
                0,
                0,
                draft.EpisodeNumber,
                draft.Title,
                draft.Content,
                null,
                draft.EndingTeaser,
                false,
                null))
            .ToList();

        return Ok(episodes);
    }

    [HttpGet("{id:long}/episodes")]
    public ActionResult<IReadOnlyList<NovelEpisodeResponse>> GetEpisodes(long id)
    {
        return Ok(new[]
        {
            new NovelEpisodeResponse(
                1,
                id,
                1,
                "Episode 1",
                "Placeholder episode content.",
                null,
                null,
                false,
                null)
        });
    }

    [HttpPost("{id:long}/publishing-plan")]
    public ActionResult<NovelPublishingPlanResponse> CreatePublishingPlan(
        long id,
        CreateNovelPublishingPlanRequest request)
    {
        return Ok(new NovelPublishingPlanResponse(
            id,
            request.TelegramChannelId,
            request.StartDate,
            request.PublishTime,
            request.CurrentEpisodeNumber,
            request.IsActive,
            true));
    }

    private static NovelResponse CreatePlaceholder(long id) => new(
        id,
        "Placeholder novel",
        null,
        "No novel has been persisted.",
        null,
        NovelStatus.Draft,
        0);
}
