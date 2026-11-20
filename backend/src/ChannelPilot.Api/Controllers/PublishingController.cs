using ChannelPilot.Api.Contracts.Publishing;
using ChannelPilot.Domain.Publishing;
using Microsoft.AspNetCore.Mvc;

namespace ChannelPilot.Api.Controllers;

[ApiController]
[Route("api/publishing")]
public sealed class PublishingController : ControllerBase
{
    [HttpGet("queue")]
    public ActionResult<IReadOnlyList<PublishQueueItemResponse>> GetQueue()
    {
        return Ok(new[] { CreatePlaceholder(1) });
    }

    [HttpGet("queue/{id:long}")]
    public ActionResult<PublishQueueItemResponse> GetQueueItem(long id)
    {
        return Ok(CreatePlaceholder(id));
    }

    [HttpPost("queue/{id:long}/approve")]
    public ActionResult<PublishQueueItemResponse> Approve(
        long id,
        ApprovePublishQueueItemRequest request)
    {
        return Ok(CreatePlaceholder(id) with
        {
            Status = PublishStatus.Approved,
            ApprovedAt = DateTime.UtcNow
        });
    }

    [HttpPost("queue/{id:long}/publish-now")]
    public ActionResult<PublishQueueItemResponse> PublishNow(long id, PublishNowRequest request)
    {
        return Ok(CreatePlaceholder(id) with
        {
            Status = PublishStatus.Published,
            PublishedAt = DateTime.UtcNow
        });
    }

    [HttpGet("history")]
    public ActionResult<IReadOnlyList<PublishQueueItemResponse>> GetHistory()
    {
        return Ok(new[]
        {
            CreatePlaceholder(1) with
            {
                Status = PublishStatus.Published,
                PublishedAt = DateTime.UnixEpoch
            }
        });
    }

    private static PublishQueueItemResponse CreatePlaceholder(long id) => new(
        id,
        1,
        1,
        null,
        "Placeholder queue item",
        "No publishing action has been executed.",
        null,
        PublishStatus.Pending,
        null,
        true,
        null,
        null,
        null,
        0);
}
