using XH_Server.Domain.Approve;
using XH_Server.Domain.Basic;

namespace XH_Server.Application;

public class BasicApplicationService<T>(
	IBasicEntityService<T> basicEntityService,
	IApproveService approveService
	)
	: IBasicApplicationService<T>
	where T : BasicEntity
{
	public long Add(T entity)
	{
		var id = basicEntityService.Create(entity);
		var template = approveService.GetTemplateByEntityName(entity.GetType().Name);
		if (template != null)
		{
			var node = approveService.GetNode(template.StartNodeId);
			approveService.Notificate(node.Id, entity.Id);
		}
		return id;
	}

	public bool Approve(long eId, long nodeId, string msg, ApprovalStatus status = ApprovalStatus.Pass)
	{
		approveService.Approve(eId, nodeId, msg, status);
		var node = approveService.JudgemntNodeCheck(nodeId, basicEntityService.GetEntityById(eId));
		if (node.NodeType == ApprovalNodeType.Approve)
		{
			approveService.Notificate(node.Id, eId);
		}
		// TODO 抄送

		return true;
	}

	public int Delete(long eId)
	{
		return basicEntityService.Delete(eId);
	}

	public IEnumerable<T> GetData()
	{
		return basicEntityService.GetEntities();
	}

	public T GetDataById(long id)
	{
		return basicEntityService.GetEntityById(id);
	}
}

