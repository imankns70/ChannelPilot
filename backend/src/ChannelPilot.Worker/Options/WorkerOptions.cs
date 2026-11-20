namespace ChannelPilot.Worker.Options;

public class WorkerOptions
{
    public bool Enabled { get; set; } = true;

    public int IntervalSeconds { get; set; } = 60;
}
