using Mapster;
using SqlSugar;
using XH_Server.Core;
using XH_Server.Domain.Dto;
using XH_Server.Domain.Entities;
using XH_Server.Domain.Vo;

namespace XH_Server.Application.Services.Application.Approval;

public class ApprovalService : IApprovalService
{
    private readonly Repository<ApprovalConfig> _repositoryConfig;
    private readonly Repository<ApprovalStep> _repositoryStep;

    public ApprovalService(Repository<ApprovalStep> approvalStepDb, Repository<ApprovalConfig> approvalConfigDb)
    {
        _repositoryStep = approvalStepDb;
        _repositoryConfig = approvalConfigDb;
    }

    public void AddConfig(ApprovalConfigDto approvalConfigDto)
    {
        _repositoryConfig.InsertReturnSnowflakeId(approvalConfigDto.Adapt<ApprovalConfig>());
    }

    public void BackStep(long stepId, string msg = "无")
    {
        var step = _repositoryStep.GetById(stepId) ?? throw new Exception("未找到对应的审核节点步骤！");

        step.ApprovalStatus = ApprovalStatus.Back;
        step.ApprovalDescription = msg;

        step.UpdateTime = DateTimeOffset.Now;

        _repositoryStep.Update(step);
    }

    public long CreateStep(ApprovalConfig approvalConfig)
    {
        var tempalte = approvalConfig.ApprovalTemplate ?? throw new Exception("模板为空");

        var lastId = 0L;
        var firstId = 0L;
        tempalte.ForEach(t =>
        {
            var approverList = (t.ApproverId ?? t.ApproverLevel) ?? throw new Exception("审核角色或审核人为空");

            approverList.ForEach(approver =>
            {
                lastId = _repositoryStep.InsertReturnSnowflakeId(new ApprovalStep
                {
                    StepType = t.StepType,
                    LastStepId = lastId,
                    ApproverId = t.ApproverId != null ? approver : null,
                    ApproverLevel = t.ApproverLevel != null ? approver : null,
                });
                firstId = firstId == 0 ? lastId : firstId;
            });

        });

        return firstId;
    }

    public void DeleteConfig(long configId)
    {
        var conf = _repositoryConfig.GetById(configId) ?? throw new Exception("未找到对应的配置");
        conf.UpdateTime = DateTimeOffset.Now;
        conf.IsDelete = true;
    }

    public (ApprovalStatus status, string msg) GetStatus(long startStepId)
    {
        var steps = _repositoryStep.Context.Queryable<ApprovalStep>()
            .ToChildList(it => it.ChildSteps, startStepId)
            .Where(it => it.StepType == StepType.Approve);

        if (steps.All(it => it.ApprovalStatus == ApprovalStatus.Pass))
        {
            return (ApprovalStatus.Pass, string.Join("\r\n",
                steps.Select(it => it.ApprovalDescription)));
        }
        else
        {
            return (ApprovalStatus.Back, string.Join("\r\n",
                steps.Where(it => it.ApprovalStatus == ApprovalStatus.Back)
                .Select(it => it.ApprovalDescription)));
        }
    }

    public void PassStep(long stepId, string msg = "无")
    {
        var step = _repositoryStep.GetById(stepId) ?? throw new Exception("未找到对应的审核节点步骤！");

        step.ApprovalStatus = ApprovalStatus.Pass;
        step.ApprovalDescription = msg;

        step.UpdateTime = DateTimeOffset.Now;

        _repositoryStep.Update(step);
    }

    public void UpdateConfig(long configId, ApprovalConfigVo approvalConfigVo)
    {
        var conf = approvalConfigVo.Adapt<ApprovalConfig>();
        conf.Id = configId;
        _repositoryConfig.Update(conf);
    }

    public (ApprovalStatus status, string msg) GetStepStatus(long stepId)
    {
        var step = _repositoryStep.GetById(stepId) ?? throw new Exception("无审核节点步骤");
        return (step.ApprovalStatus, step.ApprovalDescription);
    }

    public List<ApprovalConfig> GetConfig()
    {
        return _repositoryConfig.GetList();
    }
}
