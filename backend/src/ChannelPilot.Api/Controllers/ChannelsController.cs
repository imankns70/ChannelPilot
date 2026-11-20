using ChannelPilot.Api.Contracts.Channels;
using ChannelPilot.Application.Channels;
using ChannelPilot.Domain.Channels;
using Microsoft.AspNetCore.Mvc;

namespace ChannelPilot.Api.Controllers;

[ApiController]
[Route("api/channels")]
public sealed class ChannelsController(ChannelManagementService channelManagementService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ChannelResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var channels = await channelManagementService.GetAllAsync(cancellationToken);
        return Ok(channels.Select(ToResponse).ToList());
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ChannelResponse>> GetById(
        long id,
        CancellationToken cancellationToken)
    {
        var channel = await channelManagementService.GetByIdAsync(id, cancellationToken);
        return channel is null ? NotFound() : Ok(ToResponse(channel));
    }

    [HttpPost]
    public async Task<ActionResult<ChannelResponse>> Create(
        CreateChannelRequest request,
        CancellationToken cancellationToken)
    {
        var channel = await channelManagementService.CreateAsync(
            new TelegramChannel
            {
                Name = request.Name,
                TelegramChatId = request.TelegramChatId,
                Username = request.Username,
                Type = request.Type,
                RequiresApproval = request.RequiresApproval,
                MinScoreToPublish = request.MinScoreToPublish,
                MaxPostsPerDay = request.MaxPostsPerDay,
                MinMinutesBetweenPosts = request.MinMinutesBetweenPosts,
                FixedPublishTime = request.FixedPublishTime
            },
            cancellationToken);

        var response = ToResponse(channel);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ChannelResponse>> Update(
        long id,
        UpdateChannelRequest request,
        CancellationToken cancellationToken)
    {
        var channel = await channelManagementService.UpdateAsync(
            id,
            new TelegramChannel
            {
                Name = request.Name,
                TelegramChatId = request.TelegramChatId,
                Username = request.Username,
                Type = request.Type,
                Status = request.Status,
                RequiresApproval = request.RequiresApproval,
                MinScoreToPublish = request.MinScoreToPublish,
                MaxPostsPerDay = request.MaxPostsPerDay,
                MinMinutesBetweenPosts = request.MinMinutesBetweenPosts,
                FixedPublishTime = request.FixedPublishTime
            },
            cancellationToken);

        return channel is null ? NotFound() : Ok(ToResponse(channel));
    }

    [HttpPost("{id:long}/pause")]
    public async Task<ActionResult<ChannelResponse>> Pause(
        long id,
        CancellationToken cancellationToken)
    {
        var channel = await channelManagementService.SetStatusAsync(
            id,
            ChannelStatus.Paused,
            cancellationToken);

        return channel is null ? NotFound() : Ok(ToResponse(channel));
    }

    [HttpPost("{id:long}/activate")]
    public async Task<ActionResult<ChannelResponse>> Activate(
        long id,
        CancellationToken cancellationToken)
    {
        var channel = await channelManagementService.SetStatusAsync(
            id,
            ChannelStatus.Active,
            cancellationToken);

        return channel is null ? NotFound() : Ok(ToResponse(channel));
    }

    private static ChannelResponse ToResponse(TelegramChannel channel) => new(
        channel.Id,
        channel.Name,
        channel.TelegramChatId,
        channel.Username,
        channel.Type,
        channel.Status,
        channel.RequiresApproval,
        channel.MinScoreToPublish,
        channel.MaxPostsPerDay,
        channel.MinMinutesBetweenPosts,
        channel.FixedPublishTime);
}
