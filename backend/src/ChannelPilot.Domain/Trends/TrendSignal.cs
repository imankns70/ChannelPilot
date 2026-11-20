using ChannelPilot.Domain.Common;

namespace ChannelPilot.Domain.Trends;

public sealed class TrendSignal : Entity
{
    public long TrendNewsItemId { get; set; }

    public string SignalName { get; set; } = string.Empty;

    public decimal SignalValue { get; set; }

    public decimal Weight { get; set; }
}
