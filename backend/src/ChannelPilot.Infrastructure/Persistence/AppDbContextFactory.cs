using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChannelPilot.Infrastructure.Persistence;

public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private const string LocalConnectionString =
        "Server=.;Database=ChannelPilotDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;";

    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(LocalConnectionString)
            .Options;

        return new AppDbContext(options);
    }
}
