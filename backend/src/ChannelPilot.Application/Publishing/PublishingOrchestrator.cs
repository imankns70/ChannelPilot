using ChannelPilot.Domain.Publishing;

namespace ChannelPilot.Application.Publishing;

public sealed class PublishingOrchestrator
{
    public bool CanProcess(PublishQueueItem item, DateTime currentTime)
    {
        var hasPublishableStatus = item.Status is PublishStatus.Pending
            or PublishStatus.Approved
            or PublishStatus.Scheduled;

        return hasPublishableStatus &&
               (!item.RequiresApproval || item.ApprovedAt.HasValue) &&
               (!item.ScheduledFor.HasValue || item.ScheduledFor.Value <= currentTime);
    }
}
