using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XH_Server.Application.Services.Application.Approval;
using XH_Server.Domain.Dto;
using XH_Server.Domain.Vo;

namespace XH_Server.Controllers;

public class ApproveController : ControllerBase
{
    private readonly ApprovalService _approve;

    public ApproveController(ApprovalService approve)
    {
        _approve = approve;
    }

    [AllowAnonymous]
    public IEnumerable<string> SetApprovalConfig(IEnumerable<ApprovalConfigDto> data)
    {
        return _approve.SetConfig(data).Adapt<List<string>>();
    }

    //public IEnumerable<ApprovalConfigVo> GetApprovalConfig()
    //{
    //    return _approve.GetConfig().Select(c => c.Adapt<ApprovalConfigVo>());
    //}
}
