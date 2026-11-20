using ChannelPilot.Infrastructure;
using ChannelPilot.Application.Novels;
using ChannelPilot.Application.Channels;
using ChannelPilot.Application.ContentSources;
using ChannelPilot.Infrastructure.Persistence.Seeding;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddChannelPilotInfrastructure(builder.Configuration);
builder.Services.AddSingleton<NovelEpisodeSplitter>();
builder.Services.AddScoped<ChannelManagementService>();
builder.Services.AddScoped<ContentSourceManagementService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
    app.Logger.LogInformation("Development database seed completed.");
}

app.MapControllers();

app.MapGet("/", () => Results.Ok(new
{
    Name = "ChannelPilot.Api",
    Status = "Initial setup"
}));

app.Run();
