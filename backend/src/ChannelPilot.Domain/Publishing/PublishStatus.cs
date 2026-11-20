namespace ChannelPilot.Domain.Publishing;

public enum PublishStatus
{
    Pending,
    WaitingForApproval,
    Approved,
    Scheduled,
    Publishing,
    Published,
    Failed,
    Cancelled
}
