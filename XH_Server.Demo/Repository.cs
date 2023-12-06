using XH_Server.Domain;

namespace XH_Server.Demo;

internal class Repository<T>(ISqlSugarClient client) : IRepositoryService<T> where T : DemoEntity, new()
{
	public void CreateEntity(T entity)
	{
		client.Insertable(entity).ExecuteReturnSnowflakeId();
	}

	public IEnumerable<T> GetAllEntity()
	{
		return client.Queryable<T>().ToList();
	}

	public T GetEntityById(long id)
	{
		return client.Queryable<T>().Single(e => e.Id == id);
	}

	public void UpdateEntity(T entity)
	{
		client.Updateable(entity).ExecuteCommand();
	}
}
