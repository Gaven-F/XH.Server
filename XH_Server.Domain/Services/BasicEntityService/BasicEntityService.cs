using XH_Server.Domain.Entities;

namespace XH_Server.Domain.Services.BasicEntityService;

/// <summary>
/// 对于实体的基本操作
/// </summary>
/// <typeparam name="T">派生至基础实体接口的类</typeparam>
/// <param name="repository">仓储服务</param>
public class BasicEntityService<T>(IRepositoryService<T> repository) : IBasicEntityService<T> where T : IBasicEntity
{
	public void CreateEntity(T entity)
	{
		repository.CreateEntity(entity);
	}

	public void UpdateEntity(T entity)
	{
		entity.UpdateTime = DateTimeOffset.Now;
		repository.UpdateEntity(entity);
	}

	public void DeleteEntity(long id)
	{
		var entity = GetEntityById(id);
		entity.IsDelete = true;
		repository.UpdateEntity(entity);
	}

	public IEnumerable<T> GetAllEntities(bool hasDelete = false)
	{
		return repository.GetAllEntity().Where(e => e.IsDelete == hasDelete);
	}

	public T GetEntityById(long id)
	{
		return repository.GetEntityById(id);
	}

}
