using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InertiaDemo.Data;

public class InertiaDemoDbContextFactory : IDesignTimeDbContextFactory<InertiaDemoDbContext>
{
    public InertiaDemoDbContext CreateDbContext(string[] args)
    {
        InertiaDemoEfCoreEntityExtensionMappings.Configure();
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<InertiaDemoDbContext>()
            .UseSqlite(configuration.GetConnectionString("Default"));

        return new InertiaDemoDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}