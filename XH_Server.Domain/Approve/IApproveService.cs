namespace XH_Server.Domain.Approve;
public interface IApproveService
{
	/// <summary>
	/// 模板查询
	/// </summary>
	/// <param name="id">模板id</param>
	public void QueryTemplateById(long id);
	/// <summary>
	/// 模板查询
	/// </summary>
	/// <param name="name">对应实体/业务数据名称</param>
	public void QueryTemplateByEntityName(string name);
	/// <summary>
	/// 获取节点
	/// </summary>
	/// <param name="id">节点id</param>
	/// <remarks>单纯的获取节点信息</remarks>
	/// <returns></returns>
	public EApprovalNode GetNode(long id);
	/// <summary>
	/// 获取下一节点
	/// </summary>
	/// <param name="currentId">当前节点id</param>
	/// <remarks>尝试获取下一节点信息，若为判断节点将自动判断</remarks>
	/// <returns></returns>
	public EApprovalNode GetNextNode(long currentId);
	/// <summary>
	/// 获取实体审核最终结果
	/// </summary>
	/// <param name="entityId">实体/业务数据id</param>
	/// <returns></returns>
	public EApprovalHistory GetStatus(long entityId);
	/// <summary>
	/// 审核/创建审核记录
	/// </summary>
	/// <param name="entityId">实体/业务数据id</param>
	/// <param name="nodeId">审核的节点</param>
	/// <param name="msg">审核信息</param>
	/// <param name="status">状态</param>
	public void Approve(long entityId, long nodeId, string msg, ApprovalStatus status);
	/// <summary>
	/// 创建审核模板
	/// </summary>
	/// <param name="template"></param>
	public void CreateApprovalTemplate(EApprovalTemplate template);
	/// <summary>
	/// 创建审核节点
	/// </summary>
	/// <param name="node"></param>
	public void CreateApprocalNode(EApprovalNode node);
	/// <summary>
	/// 插入节点
	/// </summary>
	/// <param name="lastNodeId">插入位置之前的节点id</param>
	/// <param name="nodes">插入的节点</param>
	public void NodeInsertAfter(long lastNodeId, IEnumerable<EApprovalNode> nodes);
	/// <summary>
	/// 拆分节点
	/// </summary>
	/// <remarks>将node1和node2的节点关联性断开</remarks>
	/// <param name="nodeId1"></param>
	/// <param name="nodeId2"></param>
	public void BreakNode(long nodeId1, long nodeId2);
	/// <summary>
	/// 发送审核通知
	/// </summary>
	/// <param name="entityId"></param>
	/// <param name="nodeId"></param>
	public void Notificate(long entityId, long nodeId);
}
