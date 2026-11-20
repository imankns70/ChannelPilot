using ChannelPilot.Application.Abstractions.Ai;
using ChannelPilot.Application.Abstractions.Clock;
using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Application.Abstractions.Publishing;
using ChannelPilot.Application.Abstractions.Sources;
using ChannelPilot.Infrastructure.AI;
using ChannelPilot.Infrastructure.Persistence;
using ChannelPilot.Infrastructure.Persistence.Repositories;
using ChannelPilot.Infrastructure.Persistence.Seeding;
using ChannelPilot.Infrastructure.Sources;
using ChannelPilot.Infrastructure.Telegram;
using ChannelPilot.Infrastructure.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChannelPilot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddChannelPilotInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ChannelPilotDb")
            ?? throw new InvalidOperationException(
                "Connection string 'ChannelPilotDb' is not configured.");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IContentRepository, EfContentRepository>();
        services.AddScoped<IContentSourceRepository, EfContentSourceRepository>();
        services.AddScoped<IChannelRepository, EfChannelRepository>();
        services.AddScoped<INovelRepository, EfNovelRepository>();
        services.AddScoped<IPublishQueueRepository, EfPublishQueueRepository>();
        services.AddScoped<DatabaseSeeder>();
        services.AddSingleton<ITelegramPublisher, FakeTelegramPublisher>();
        services.AddSingleton<IContentSourceReader, FakeContentSourceReader>();
        services.AddSingleton<IAiContentAnalyzer, FakeAiContentAnalyzer>();
        services.AddSingleton<IAiContentRewriter, FakeAiContentRewriter>();

        return services;
    }
}
