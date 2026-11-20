using ChannelPilot.Domain.Channels;
using ChannelPilot.Domain.Novels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChannelPilot.Infrastructure.Persistence.Configurations;

public sealed class NovelPublishingPlanConfiguration : IEntityTypeConfiguration<NovelPublishingPlan>
{
    public void Configure(EntityTypeBuilder<NovelPublishingPlan> builder)
    {
        builder.ToTable("NovelPublishingPlans");
        builder.HasKey(plan => plan.Id);

        builder.HasOne<Novel>()
            .WithMany()
            .HasForeignKey(plan => plan.NovelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<TelegramChannel>()
            .WithMany()
            .HasForeignKey(plan => plan.TelegramChannelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(plan => plan.NovelId).IsUnique();
    }
}
