using SqlSugar;

namespace XH_Server.Core.Database;
public class RepositoryService<T>(DatabaseService databaseService) : SimpleClient<T>(databaseService.Instance)
	where T : class, new()
{
}
