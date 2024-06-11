using LogApi.Services.Persistence;

namespace LogApi.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var sqlServerOptions = configuration.GetSection(SqlServerOptions.sectionKey).Get<SqlServerOptions>();

        serviceCollection.addMsSqlCollection(sqlServerOptions!);

        return serviceCollection;
    }
}