
using SqlSugar;
using XH_Server.Core.Database;
using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Approve;

public class ApprovalService(DatabaseService dbService) : IApproveService
{
	private readonly ISqlSugarClient db = dbService.Instance;

	public long Approve(long entityId, long nodeId, string msg, ApprovalStatus status)
	{
		return db.Insertable(new EApprovalHistory()
		{
			NodeId = nodeId,
			ApprovalEntityId = entityId,
			Msg = msg,
			Status = status
		}).ExecuteReturnSnowflakeId();
	}

	public void BreakNode(long nodeId1, long nodeId2)
	{
		var node1 = GetNode(nodeId1);
		var node2 = GetNode(nodeId2);
		node1.UpdateTime = DateTime.Now;
		node2.UpdateTime = DateTime.Now;
		node1.NextNodeId = node1.NextNodeId == nodeId2 ? 0 : node1.NextNodeId;
		node2.NextNodeId = node2.NextNodeId == nodeId1 ? 0 : node2.NextNodeId;
		db.Updateable(new List<EApprovalNode> { node1, node2 }).ExecuteCommand();
	}

	public long CreateApprocalNode(EApprovalNode node, long templateId)
	{
		node.ApprovalTemplateId = templateId;
		return db.Insertable(node).ExecuteReturnSnowflakeId();
	}

	public long CreateApprovalTemplate(EApprovalTemplate template)
	{
		return db.Insertable(template).ExecuteReturnSnowflakeId();
	}

	public EApprovalNode JudgemntNodeCheck<T>(long nodeId, T data) where T : BasicEntity
	{
		var currentNode = db.Queryable<EApprovalNode>().Single(e => e.Id == nodeId);
		switch (currentNode.NodeType)
		{
			case ApprovalNodeType.Approve:
			case ApprovalNodeType.Copy:
				return GetNode(currentNode.Id);
			case ApprovalNodeType.Judge:
				var conditions = db.Queryable<ECondition>()
					.Where(e => e.NodeId == currentNode.Id && !e.IsDeleted)
					.ToList();
				var validNodeId = conditions.Select(c =>
				{
					if (c.Condition.Check(data))
					{
						return c.PassNodeId;
					}
					else
					{
						return c.BackNodeId;
					}
				}).ToList()[0];
				return GetNode(validNodeId);
			//if(validNodeId.Count > 0)
			//{
			//	throw new Exception("分支路径过多！");
			//}
			default:
				throw new Exception("类型错误！");
		}
	}

	public EApprovalNode GetNode(long id)
	{
		return db.Queryable<EApprovalNode>().Includes(e => e.Histories).Single(e => e.Id == id && !e.IsDeleted);
	}

	public IEnumerable<EApprovalHistory> GetStatus(long entityId)
	{
		var histories = db.Queryable<EApprovalHistory>()
			.Where(e => !e.IsDeleted && e.ApprovalEntityId == entityId)
			.Includes(e => e.Node)
			.OrderBy(e => e.Node!.CreateTime)
			.ToList();

		return histories;
	}


	public EApprovalTemplate GetTemplateByEntityName(string name)
	{
		return db.Queryable<EApprovalTemplate>().Single(e => e.BindEntity == name);
	}

	public EApprovalTemplate GetTemplateById(long id)
	{
		return db.Queryable<EApprovalTemplate>().InSingle(id);
	}

	public void NodeInsertAfter(long lastNodeId, IEnumerable<EApprovalNode> nodes)
	{
		db.Ado.UseTran(() =>
		{
			var node = db.Queryable<EApprovalNode>().First(e => e.Id == lastNodeId);
			var nextNodeId = node.NextNodeId;
			nodes.ToList().ForEach(e =>
			{
				node.NextNodeId = db.Insertable(e).ExecuteReturnSnowflakeId();
				node.UpdateTime = DateTime.Now;
				db.Updateable(node).ExecuteCommand();
				node = db.Queryable<EApprovalNode>().First(e => e.Id == node.NextNodeId);
			});
			node.NextNodeId = nextNodeId;
			node.UpdateTime = DateTime.Now;
			db.Updateable(node).ExecuteCommand();
		});

	}

	// TODO 通知接口
	public void Notificate(long entityId, long nodeId)
	{
		Console.WriteLine("通知！");
	}
}
