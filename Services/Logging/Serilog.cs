using System.Data;
using LogApi.Constants;
using LogApi.Services.Persistence;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace LogApi.Services.Logging;

public static class Serilog
{
    public static IHostBuilder useSerilogLoggingService(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
            loggerConfiguration.configureSerilog(hostBuilderContext.Configuration));

        return hostBuilder;
    }

    private static LoggerConfiguration configureSerilog(this LoggerConfiguration loggerConfiguration,
        IConfiguration configuration)
    {
        var appInfoOptions = configuration.GetSection(AppInfoConstant.sectionKey).Get<AppInfoConstant>();
        var sqlServerOptions = configuration.GetSection(SqlServerOptions.sectionKey).Get<SqlServerOptions>();

        return loggerConfiguration
            .ReadFrom.Configuration(configuration, sectionName: nameof(Serilog))
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(x => x.MessageTemplate.Text.Contains("Request"))
                .WriteTo.MSSqlServer(
                    connectionString: sqlServerOptions!.connectionString,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "LogRequest",
                        AutoCreateSqlTable = true
                    },
                    columnOptions: new ColumnOptions
                    {
                        AdditionalColumns = new List<SqlColumn>
                        {
                            new()
                            {
                                ColumnName = "UniqueId",
                                DataType = SqlDbType.NVarChar,
                                DataLength = 50
                            }
                        }
                    }))
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(x => x.MessageTemplate.Text.Contains("Response"))
                .WriteTo.MSSqlServer(
                    connectionString: sqlServerOptions!.connectionString,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "LogResponse",
                        AutoCreateSqlTable = true
                    },
                    columnOptions: new ColumnOptions
                    {
                        AdditionalColumns = new List<SqlColumn>
                        {
                            new()
                            {
                                ColumnName = "UniqueId",
                                DataType = SqlDbType.NVarChar,
                                DataLength = 50
                            }
                        }
                    }));
    }
}