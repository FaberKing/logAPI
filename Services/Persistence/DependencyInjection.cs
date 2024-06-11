using LogApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LogApi.Services.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection addMsSqlCollection(this IServiceCollection service,
        SqlServerOptions sqlServerOptions)
    {
        var migrationAssembly = typeof(SqlServerDbContext).Assembly.FullName;
        
        service.AddDbContext<SqlServerDbContext>(options =>
        {
            options.UseSqlServer(sqlServerOptions.connectionString, builder =>
            {
                builder.MigrationsAssembly(migrationAssembly);
                builder.MigrationsHistoryTable("SchemaVersions", nameof(LogApi));
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            options.ConfigureWarnings(wcb => wcb.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            options.ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        });

        service.AddScoped<SqlServerDbContextInterface>(provider => provider.GetRequiredService<SqlServerDbContext>());

        return service;
    }
}