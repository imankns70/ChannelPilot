using ChannelPilot.Application.Abstractions.Clock;

namespace ChannelPilot.Infrastructure.Time;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Now => DateTime.Now;

    public DateOnly Today => DateOnly.FromDateTime(Now);

    public TimeOnly CurrentTime => TimeOnly.FromDateTime(Now);
}
