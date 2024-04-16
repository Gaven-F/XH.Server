namespace Server.Core.Config;

public class DatabaseConfig
{
    public string ConnectionString { get; set; } = "datasource=data.sqlite";
    public string DatabaseType { get; set; } = "Sqlite";
}
