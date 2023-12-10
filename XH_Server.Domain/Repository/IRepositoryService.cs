using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Repository;

public interface IRepositoryService<T> where T : Basic.BasicEntity
{
	T GetDataById(long id);
	IEnumerable<T> GetData(bool isDelete);
	long SaveData(T e);
	int UpdateData(T e);
	int DeleteData(T e);
}
