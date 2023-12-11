using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Repository;

public interface IRepositoryService<T> where T : BasicEntity, new()
{
	T GetDataById(long id);
	IEnumerable<T> GetData(bool isDelete);
	long SaveData(T e);
	int UpdateData(T e);
	int DeleteData(long eId);
}
