
namespace XH_Server.Domain.Basic;

// TODO 实现接口
public class BasicEntityService<T> : IBasicEntityService<T> where T : BasicEntity
{
	public long Create(T e)
	{
		throw new NotImplementedException();
	}

	public int Delete(T e)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<T> GetEntities(bool isDelete = false)
	{
		throw new NotImplementedException();
	}

	public T GetEntityById(long id)
	{
		throw new NotImplementedException();
	}

	public int Update(T e)
	{
		throw new NotImplementedException();
	}
}
