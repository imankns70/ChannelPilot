using ChannelPilot.Domain.Common;

namespace ChannelPilot.Domain.Novels;

public sealed class NovelEpisode : AuditableEntity
{
    public long NovelId { get; set; }

    public int EpisodeNumber { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string? HookText { get; set; }

    public string? EndingTeaser { get; set; }

    public bool IsPublished { get; set; }

    public DateTime? PublishedAt { get; set; }
}
