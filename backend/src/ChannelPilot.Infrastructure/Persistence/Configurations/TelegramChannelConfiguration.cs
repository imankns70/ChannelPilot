using ChannelPilot.Domain.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChannelPilot.Infrastructure.Persistence.Configurations;

public sealed class TelegramChannelConfiguration : IEntityTypeConfiguration<TelegramChannel>
{
    public void Configure(EntityTypeBuilder<TelegramChannel> builder)
    {
        builder.ToTable("TelegramChannels");
        builder.HasKey(channel => channel.Id);

        builder.Property(channel => channel.Name).HasMaxLength(200).IsRequired();
        builder.Property(channel => channel.Username).HasMaxLength(100);
        builder.Property(channel => channel.Type).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(channel => channel.Status).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(channel => channel.MinScoreToPublish).HasPrecision(5, 2);

        builder.HasIndex(channel => channel.TelegramChatId).IsUnique();
    }
}
