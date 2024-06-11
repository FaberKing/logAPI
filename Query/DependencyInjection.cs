using LogApi.Interfaces;

namespace LogApi.Query;

public static class DependencyInjection
{
    public static IServiceCollection AddQueriesService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<UserQueryInterface, UserQuery>();

        return serviceCollection;
    }
}