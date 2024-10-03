using Volo.Abp.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace InertiaDemo.Data;

public class InertiaDemoDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public InertiaDemoDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        
        /* We intentionally resolving the InertiaDemoDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<InertiaDemoDbContext>()
            .Database
            .MigrateAsync();

    }
}
