using XH_Server.Domain.Entities;

namespace XH_Server.Domain.Services.BasicEntityService;
/// <summary>
/// 实体的基础操作服务接口
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBasicEntityService<T> where T : IBasicEntity
{
	/// <summary>
	/// 获取实体
	/// </summary>
	/// <param name="id">实体的唯一Id</param>
	T GetEntityById(long id);
	/// <summary>
	/// 创建新的实体并进行存储
	/// </summary>
	void CreateEntity(T entity);
	/// <summary>
	/// 更新实体
	/// </summary>
	/// <param name="entity">更新后的实体</param>
	void UpdateEntity(T entity);
	/// <summary>
	/// 删除实体
	/// </summary>
	/// <param name="id">实体Id</param>
	void DeleteEntity(long id);
	IEnumerable<T> GetAllEntities(bool hasDelete = false);
}
