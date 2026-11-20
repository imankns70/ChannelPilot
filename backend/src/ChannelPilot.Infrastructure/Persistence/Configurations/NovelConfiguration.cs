using ChannelPilot.Domain.Novels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChannelPilot.Infrastructure.Persistence.Configurations;

public sealed class NovelConfiguration : IEntityTypeConfiguration<Novel>
{
    public void Configure(EntityTypeBuilder<Novel> builder)
    {
        builder.ToTable("Novels");
        builder.HasKey(novel => novel.Id);

        builder.Property(novel => novel.Title).HasMaxLength(500).IsRequired();
        builder.Property(novel => novel.Author).HasMaxLength(300);
        builder.Property(novel => novel.Description).HasMaxLength(2_000);
        builder.Property(novel => novel.Genre).HasMaxLength(100);
        builder.Property(novel => novel.Status).HasConversion<string>().HasMaxLength(30).IsRequired();
    }
}
