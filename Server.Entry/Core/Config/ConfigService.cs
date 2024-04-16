namespace Server.Core.Config;

public class ConfigService
{
    private static readonly string configFileName = "xh.config.json";

    private readonly IConfiguration? _rootConfiguration;

    public AppConfig AppConfig { get; private set; }
    public DatabaseConfig DatabaseConfig { get; private set; }
    public DingTalkConfig DingTalkConfig { get; private set; }

    public IConfiguration RootConfiguration =>
        _rootConfiguration ?? throw new Exception("未生成根配置项，请确认配置文件是否存在！");

    public ConfigService()
    {
        if (File.Exists(configFileName))
        {
            _rootConfiguration = new ConfigurationBuilder().AddJsonFile(configFileName).Build();

            AppConfig =
                _rootConfiguration.GetSection(nameof(AppConfig)).Get<AppConfig>()
                ?? new AppConfig();
            DatabaseConfig =
                _rootConfiguration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>()
                ?? new DatabaseConfig();
            DingTalkConfig =
                _rootConfiguration.GetSection(nameof(DingTalkConfig)).Get<DingTalkConfig>()
                ?? new DingTalkConfig();
        }
        else
        {
            AppConfig = new AppConfig();
            DatabaseConfig = new DatabaseConfig();
            DingTalkConfig = new DingTalkConfig();
        }
    }
}
