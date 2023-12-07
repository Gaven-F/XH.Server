using XH_Server.Core.Database;
using XH_Server.Domain.Basic;

namespace XH_Server.Domain.DataEntities;
public class BasicEntityService<T>(DatabaseService dbService) where T : BasicEntity
{
	public DatabaseService DbService { get; set; } = dbService;
	public long Save(T e)
	{
		return e.Save(DbService);
	}

	public int Update(T e)
	{
		return e.Update(DbService);
	}

	public int Delete(T e)
	{
		return e.Delete(DbService);
	}

	public T GetEntityById (long id)
	{
		return DbService.Instance.Queryable<T>().Single(e => e.Id == id);
	}
}
