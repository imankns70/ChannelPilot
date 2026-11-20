using ChannelPilot.Domain.Content;
using ChannelPilot.Domain.Football;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChannelPilot.Infrastructure.Persistence.Configurations;

public sealed class FootballNewsItemConfiguration : IEntityTypeConfiguration<FootballNewsItem>
{
    public void Configure(EntityTypeBuilder<FootballNewsItem> builder)
    {
        builder.ToTable("FootballNewsItems");
        builder.HasKey(item => item.Id);

        builder.Property(item => item.Category).HasConversion<string>().HasMaxLength(40).IsRequired();
        builder.Property(item => item.ImportanceLevel).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(item => item.MainEntity).HasMaxLength(300);
        builder.Property(item => item.Reason).HasMaxLength(1_000);

        builder.HasOne<ContentItem>()
            .WithMany()
            .HasForeignKey(item => item.ContentItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(item => item.ContentItemId).IsUnique();
    }
}
