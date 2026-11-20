using ChannelPilot.Domain.Content;
using ChannelPilot.Domain.Trends;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChannelPilot.Infrastructure.Persistence.Configurations;

public sealed class TrendNewsItemConfiguration : IEntityTypeConfiguration<TrendNewsItem>
{
    public void Configure(EntityTypeBuilder<TrendNewsItem> builder)
    {
        builder.ToTable("TrendNewsItems");
        builder.HasKey(item => item.Id);

        builder.Property(item => item.Category).HasConversion<string>().HasMaxLength(40).IsRequired();
        builder.Property(item => item.TrendScore).HasPrecision(5, 2);
        builder.Property(item => item.Reason).HasMaxLength(1_000);

        builder.HasOne<ContentItem>()
            .WithMany()
            .HasForeignKey(item => item.ContentItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(item => item.ContentItemId).IsUnique();
    }
}
