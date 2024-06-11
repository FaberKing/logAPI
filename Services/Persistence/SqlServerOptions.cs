namespace LogApi.Services.Persistence;

public record SqlServerOptions
{
    public static readonly string sectionKey = $"{nameof(Persistence)}:SqlServer";

    public string connectionString { get; set; } = default!;
}