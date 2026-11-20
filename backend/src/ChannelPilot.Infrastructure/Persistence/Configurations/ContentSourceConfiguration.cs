using ChannelPilot.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChannelPilot.Infrastructure.Persistence.Configurations;

public sealed class ContentSourceConfiguration : IEntityTypeConfiguration<ContentSource>
{
    public void Configure(EntityTypeBuilder<ContentSource> builder)
    {
        builder.ToTable("ContentSources");
        builder.HasKey(source => source.Id);

        builder.Property(source => source.Name).HasMaxLength(200).IsRequired();
        builder.Property(source => source.BaseUrl).HasMaxLength(2_000);
        builder.Property(source => source.SourceKind).HasMaxLength(50).IsRequired();
        builder.Property(source => source.ReliabilityScore).HasPrecision(5, 2);
    }
}
