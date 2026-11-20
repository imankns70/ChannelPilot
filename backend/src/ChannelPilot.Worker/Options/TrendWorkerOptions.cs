namespace ChannelPilot.Worker.Options;

public sealed class TrendWorkerOptions : WorkerOptions
{
    public const string SectionName = "Workers:Trend";

    public TrendWorkerOptions()
    {
        IntervalSeconds = 600;
    }
}
