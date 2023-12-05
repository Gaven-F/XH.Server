namespace XH_Server.Core.Config.ConfigType;
public class DatabaseConfig
{
    public string ConnectionString { get; set; } = "data.sqlite";
    public string DatabaseType { get; set; } = "sqlite";
}
