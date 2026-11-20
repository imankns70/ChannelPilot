using ChannelPilot.Domain.Trends;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChannelPilot.Infrastructure.Persistence.Configurations;

public sealed class TrendSignalConfiguration : IEntityTypeConfiguration<TrendSignal>
{
    public void Configure(EntityTypeBuilder<TrendSignal> builder)
    {
        builder.ToTable("TrendSignals");
        builder.HasKey(signal => signal.Id);

        builder.Property(signal => signal.SignalName).HasMaxLength(200).IsRequired();
        builder.Property(signal => signal.SignalValue).HasPrecision(18, 4);
        builder.Property(signal => signal.Weight).HasPrecision(9, 4);

        builder.HasOne<TrendNewsItem>()
            .WithMany()
            .HasForeignKey(signal => signal.TrendNewsItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
