using XH_Server.Domain.Repository;

namespace XH_Server.Domain.Basic;

public class BasicEntityService<T>(IRepositoryService<T> repositoryService) : IBasicEntityService<T> where T : BasicEntity, new()
{
	public long Create(T e)
	{
		return repositoryService.SaveData(e);
	}

	public int Delete(long eId)
	{
		return repositoryService.DeleteData(eId);
	}

	public IEnumerable<T> GetEntities(bool isDelete = false)
	{
		return repositoryService.GetData(isDelete);
	}

	public T GetEntityById(long id)
	{
		return repositoryService.GetDataById(id);
	}

	public int Update(T e)
	{
		return repositoryService.UpdateData(e);
	}
}
