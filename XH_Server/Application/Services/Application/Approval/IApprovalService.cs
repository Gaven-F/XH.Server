using XH_Server.Domain.Dto;
using XH_Server.Domain.Entities;
using XH_Server.Domain.Vo;

namespace XH_Server.Application.Services.Application.Approval;

public interface IApprovalService
{
    /// <summary>
    /// 创建审核流程节点步骤
    /// </summary>
    /// <param name="approvalConfig">审核配置</param>
    /// <returns>首节点Id</returns>
    public long CreateStep(ApprovalConfig approvalConfig);
    /// <summary>
    /// 获取审核流程配置
    /// </summary>
    /// <returns></returns>
    public List<ApprovalConfig> GetConfig();
    /// <summary>
    /// 通过审核
    /// </summary>
    /// <param name="stepId">步骤的d</param>
    /// <param name="msg">审核留言</param>
    public void PassStep(long stepId, string msg = "无");
    /// <summary>
    /// 退回审核
    /// </summary>
    /// <param name="stepId">步骤Id</param>
    /// <param name="msg">审核留言</param>
    public void BackStep(long stepId, string msg = "无");
    /// <summary>
    /// 获取步骤状态
    /// </summary>
    /// <returns></returns>
    public (ApprovalStatus status, string msg) GetStepStatus(long stepId);
    /// <summary>
    /// 获取审核状态
    /// </summary>
    /// <returns></returns>
    public (ApprovalStatus status, string msg) GetStatus(long startStepId);
    /// <summary>
    /// 创建审核配置
    /// </summary>
    /// <param name="approvalConfigDto"></param>
    public void AddConfig(ApprovalConfigDto approvalConfigDto);
    /// <summary>
    /// 删除审核配置
    /// </summary>
    /// <param name="configId"></param>
    public void DeleteConfig(long configId);
    /// <summary>
    /// 更新审核配置
    /// </summary>
    /// <param name="configId"></param>
    /// <param name="approvalConfigVo"></param>
    public void UpdateConfig(long configId, ApprovalConfigVo approvalConfigVo);
}
