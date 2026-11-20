using ChannelPilot.Api.Contracts.Trends;
using ChannelPilot.Domain.Trends;
using Microsoft.AspNetCore.Mvc;

namespace ChannelPilot.Api.Controllers;

[ApiController]
[Route("api/trends")]
public sealed class TrendsController : ControllerBase
{
    [HttpGet("news")]
    public ActionResult<IReadOnlyList<TrendNewsResponse>> GetNews()
    {
        return Ok(new[] { CreatePlaceholder(1) });
    }

    [HttpGet("news/{id:long}")]
    public ActionResult<TrendNewsResponse> GetNewsById(long id)
    {
        return Ok(CreatePlaceholder(id));
    }

    [HttpPost("news/{id:long}/review")]
    public ActionResult<TrendNewsResponse> Review(long id, ReviewTrendRequest request)
    {
        return Ok(CreatePlaceholder(id) with
        {
            RequiresManualReview = false,
            Reason = request.Reason ?? (request.Approved ? "Placeholder approval." : "Placeholder rejection.")
        });
    }

    private static TrendNewsResponse CreatePlaceholder(long id) => new(
        id,
        id,
        "Placeholder trend news",
        TrendCategory.General,
        0,
        false,
        false,
        true,
        "No trend analysis has been run.");
}
