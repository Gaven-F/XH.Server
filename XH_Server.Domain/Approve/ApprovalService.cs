namespace XH_Server.Domain.Approve;

public class ApprovalService : IApproveService
{
	public void Approve(long entityId, long nodeId, string msg, ApprovalStatus status)
	{
		throw new NotImplementedException();
	}

	public void BreakNode(long nodeId1, long nodeId2)
	{
		throw new NotImplementedException();
	}

	public void CreateApprocalNode(EApprovalNode node)
	{
		throw new NotImplementedException();
	}

	public void CreateApprovalTemplate(EApprovalTemplate template)
	{
		throw new NotImplementedException();
	}

	public EApprovalNode GetNextNode(long currentId)
	{
		throw new NotImplementedException();
	}

	public EApprovalNode GetNode(long id)
	{
		throw new NotImplementedException();
	}

	public EApprovalHistory GetStatus(long entityId)
	{
		throw new NotImplementedException();
	}

	public void NodeInsertAfter(long lastNodeId, IEnumerable<EApprovalNode> nodes)
	{
		throw new NotImplementedException();
	}

	public void Notificate(long entityId, long nodeId)
	{
		throw new NotImplementedException();
	}

	public void QueryTemplateByEntityName(string name)
	{
		throw new NotImplementedException();
	}

	public void QueryTemplateById(long id)
	{
		throw new NotImplementedException();
	}
}
