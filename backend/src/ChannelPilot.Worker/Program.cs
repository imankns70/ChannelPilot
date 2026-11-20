using ChannelPilot.Infrastructure;
using ChannelPilot.Worker.Jobs;
using ChannelPilot.Worker.Options;
using ChannelPilot.Worker.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddChannelPilotInfrastructure(builder.Configuration);

builder.Services.Configure<FootballWorkerOptions>(
    builder.Configuration.GetSection(FootballWorkerOptions.SectionName));
builder.Services.Configure<TrendWorkerOptions>(
    builder.Configuration.GetSection(TrendWorkerOptions.SectionName));
builder.Services.Configure<NovelWorkerOptions>(
    builder.Configuration.GetSection(NovelWorkerOptions.SectionName));
builder.Services.Configure<PublishQueueWorkerOptions>(
    builder.Configuration.GetSection(PublishQueueWorkerOptions.SectionName));

builder.Services.AddSingleton<CollectFootballNewsJob>();
builder.Services.AddSingleton<CollectTrendNewsJob>();
builder.Services.AddSingleton<PublishNovelEpisodeJob>();
builder.Services.AddSingleton<PublishQueuedPostsJob>();

builder.Services.AddHostedService<FootballNewsWorker>();
builder.Services.AddHostedService<TrendNewsWorker>();
builder.Services.AddHostedService<NovelPublishingWorker>();
builder.Services.AddHostedService<PublishQueueWorker>();

var host = builder.Build();
host.Run();
