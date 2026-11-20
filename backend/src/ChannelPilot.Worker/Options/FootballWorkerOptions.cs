namespace ChannelPilot.Worker.Options;

public sealed class FootballWorkerOptions : WorkerOptions
{
    public const string SectionName = "Workers:Football";

    public FootballWorkerOptions()
    {
        IntervalSeconds = 300;
    }
}
