using XH_Server.Domain.Approve;
using XH_Server.Domain.Basic;

namespace XH_Server.Application;
public interface IBasicApplicationService<T> where T : BasicEntity
{
	long Add(T entity);
	int Delete(long eId);
	bool Approve(long eId, long nodeId, string msg, ApprovalStatus status);
	IEnumerable<T> GetData();
	T GetDataById(long id);
}

