using XH_Server.Domain.Entities;

namespace XH_Server.Domain;

public interface IRepositoryService<T> where T :IBasicEntity
{
	void CreateEntity(T entity);
	IEnumerable<T> GetAllEntity();
	T GetEntityById(long id);
	void UpdateEntity(T entity);
}