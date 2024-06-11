namespace LogApi.Constants;

public record AppInfoConstant
{
    public const string sectionKey = "AppInfo";
    public string name { get; set; } = default!;
}