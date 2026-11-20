using ChannelPilot.Domain.Common;

namespace ChannelPilot.Domain.Football;

public sealed class FootballNewsItem : Entity
{
    public long ContentItemId { get; set; }

    public FootballNewsCategory Category { get; set; }

    public FootballImportanceLevel ImportanceLevel { get; set; }

    public string? MainEntity { get; set; }

    public bool IsTransferRelated { get; set; }

    public bool IsBreakingNews { get; set; }

    public bool IsSensitive { get; set; }

    public string? Reason { get; set; }
}
