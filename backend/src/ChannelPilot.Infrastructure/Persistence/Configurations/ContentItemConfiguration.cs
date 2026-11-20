using ChannelPilot.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChannelPilot.Infrastructure.Persistence.Configurations;

public sealed class ContentItemConfiguration : IEntityTypeConfiguration<ContentItem>
{
    public void Configure(EntityTypeBuilder<ContentItem> builder)
    {
        builder.ToTable("ContentItems");
        builder.HasKey(content => content.Id);

        builder.Property(content => content.Type).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(content => content.Status).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(content => content.OriginalTitle).HasMaxLength(500).IsRequired();
        builder.Property(content => content.OriginalSummary).HasMaxLength(2_000);
        builder.Property(content => content.SourceUrl).HasMaxLength(2_000);
        builder.Property(content => content.ImageUrl).HasMaxLength(2_000);
        builder.Property(content => content.SourceName).HasMaxLength(200);
        builder.Property(content => content.NormalizedTitle).HasMaxLength(500);
        builder.Property(content => content.ContentHash).HasMaxLength(128);

        builder.OwnsOne(content => content.QualityScore, score =>
        {
            score.Property(value => value.ImportanceScore).HasColumnName("ImportanceScore").HasPrecision(5, 2);
            score.Property(value => value.AttractivenessScore).HasColumnName("AttractivenessScore").HasPrecision(5, 2);
            score.Property(value => value.FreshnessScore).HasColumnName("FreshnessScore").HasPrecision(5, 2);
            score.Property(value => value.ReliabilityScore).HasColumnName("ReliabilityScore").HasPrecision(5, 2);
            score.Property(value => value.FinalScore).HasColumnName("FinalScore").HasPrecision(5, 2);
        });
        builder.Navigation(content => content.QualityScore).IsRequired(false);

        builder.HasOne<ContentSource>()
            .WithMany()
            .HasForeignKey(content => content.SourceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(content => content.ContentHash);
        builder.HasIndex(content => new { content.Status, content.CollectedAt });
    }
}
