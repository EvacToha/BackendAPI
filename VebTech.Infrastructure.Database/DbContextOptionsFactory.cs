using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VebTech.Infrastructure.Database;

public static class DbContextOptionsFactory
{
    public static DbContextOptions<DatabaseContext> Get()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Directory.GetCurrentDirectory() + "/appsettings.json").Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseSqlServer(connectionString);
        return builder.Options;
    }
}