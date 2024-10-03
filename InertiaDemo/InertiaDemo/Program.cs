using System;
using InertiaDemo.Data;
using Serilog;
using Serilog.Events;
using Volo.Abp.Data;

namespace InertiaDemo;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog((context, services, loggerConfiguration) =>
                {
                    loggerConfiguration
#if DEBUG
                        .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .WriteTo.Async(c => c.File("Logs/logs.txt"))
                        .WriteTo.Async(c => c.Console());
                    
                    if (IsMigrateDatabase(args))
                    {
                        loggerConfiguration.MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning);
                        loggerConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                    }
                    else
                    {
                        loggerConfiguration.WriteTo.Async(c => c.AbpStudio(services));
                    }
                });
            if (IsMigrateDatabase(args))
            {
                builder.Services.AddDataMigrationEnvironment();
            }
            await builder.AddApplicationAsync<InertiaDemoModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();

            if (IsMigrateDatabase(args))
            {
                await app.Services.GetRequiredService<InertiaDemoDbMigrationService>().MigrateAsync();
                return 0;
            }

            Log.Information("Starting InertiaDemo.");
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "InertiaDemo terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static bool IsMigrateDatabase(string[] args)
    {
        return args.Any(x => x.Contains("--migrate-database", StringComparison.OrdinalIgnoreCase));
    }
}
