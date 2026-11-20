using ChannelPilot.Domain.Common;

namespace ChannelPilot.Domain.Novels;

public sealed class Novel : AuditableEntity
{
    public string Title { get; set; } = string.Empty;

    public string? Author { get; set; }

    public string? Description { get; set; }

    public string? Genre { get; set; }

    public NovelStatus Status { get; set; }

    public int TotalEpisodes { get; set; }
}
