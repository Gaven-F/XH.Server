
using XH.Core.Database.Entities.ApprovalEntities;
using XH.Core.DataBase.Entities;
using XH.Core.Interfaces.AsTools;

namespace XH.Application.Work;
public interface IApproveService
{
	/// <summary>
	/// 创建审核流程配置至数据库
	/// </summary>
	[Obsolete("目前只是设想，暂未完成")]
	public void CreateApprovalConfig();
	/// <summary>
	/// 通过审核流程配置创建审核步骤至数据库
	/// </summary>
	/// <param name="config">审核流程配置</param>
	public void CreateApprovalStepByConfig(string config);
	/// <summary>
	/// 绑定
	/// </summary>
	/// <typeparam name="T">需要审核的实体类型</typeparam>
	public void BindApprocal<T>() where T : BaseEntity, INeedApprove, new();
	/// <summary>
	/// 审核步骤状态改变
	/// </summary>
	/// <param name="approvalId">状态改变的审核步骤Id</param>
	/// <param name="status">状态</param>
	public void Approve(long approvalId, ApprovalStatusEnum status);
	/// <summary>
	/// 通过审核的审核步骤
	/// </summary>
	/// <param name="approvalId">审核步骤Id</param>
	/// <param name="msg">审核反馈</param>
	public void ApprovePass(long approvalId, string msg = "无");
	/// <summary>
	/// 退回审核的审核步骤
	/// </summary>
	/// <param name="approvalId">审核步骤Id</param>
	/// <param name="msg">审核反馈</param>
	public void ApproveBack(long approvalId, string msg = "无");
	/// <summary>
	/// 是否通过审核
	/// </summary>
	/// <param name="entryId">实体Id</param>
	/// <returns></returns>
	public (bool isApproved, string msg) IsApproved(long entryId);
}
