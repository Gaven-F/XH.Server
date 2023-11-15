using Mapster;
using Microsoft.AspNetCore.Mvc;
using XH_Server.Application.Services.Application.Approval;
using XH_Server.Domain.Dto;
using XH_Server.Domain.Vo;

namespace XH_Server.Controllers;

public class ApproveController : ControllerBase
{
    private readonly IApprovalService _approve;

    public ApproveController(IApprovalService approve)
    {
        _approve = approve;
    }

    public void AddApprovalConfig(ApprovalConfigDto data)
    {
        _approve.AddConfig(data);
    }

    public IEnumerable<ApprovalConfigVo> GetApprovalConfig()
    {
        return _approve.GetConfig().Select(c => c.Adapt<ApprovalConfigVo>());
    }
}
