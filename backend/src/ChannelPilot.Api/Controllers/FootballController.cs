using ChannelPilot.Api.Contracts.Football;
using ChannelPilot.Domain.Football;
using Microsoft.AspNetCore.Mvc;

namespace ChannelPilot.Api.Controllers;

[ApiController]
[Route("api/football")]
public sealed class FootballController : ControllerBase
{
    [HttpGet("news")]
    public ActionResult<IReadOnlyList<FootballNewsResponse>> GetNews()
    {
        return Ok(new[] { CreatePlaceholder(1) });
    }

    [HttpGet("news/{id:long}")]
    public ActionResult<FootballNewsResponse> GetNewsById(long id)
    {
        return Ok(CreatePlaceholder(id));
    }

    private static FootballNewsResponse CreatePlaceholder(long id) => new(
        id,
        id,
        "Placeholder football news",
        FootballNewsCategory.General,
        FootballImportanceLevel.Medium,
        null,
        false,
        false,
        false,
        "No football analysis has been run.");
}
