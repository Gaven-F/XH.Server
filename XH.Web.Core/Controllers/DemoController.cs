using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XH.Application.Ali;
using XH.Application.System.Services;

namespace XH.Web.Core.Controllers;

/// <summary>
/// 测试服务接口
/// </summary>
public class DemoController : IDynamicApiController
{
    private readonly ISystemService _systemService;
    private readonly DTService _dTContext;
    private readonly OSSService _ossService;

    public DemoController(ISystemService systemService, DTService dTContext, OSSService ossService)
    {
        _systemService = systemService;
        _dTContext = dTContext;
        _ossService = ossService;
    }

    /// <summary>
    /// 获取系统描述
    /// </summary>
    /// <returns></returns>
    public string GetDescription()
    {
        return _systemService.GetDescription();
    }

    /// <summary>
    /// 数据库初始化
    /// </summary>
    public void InitDataBase()
    {
        _systemService.DataBaseInit();
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="code">免登码</param>
    /// <returns></returns>
    public string GetUserInfo(string code)
    {
        return _dTContext.GetUserInfo(code);
    }

    public void PostFile([FromForm] List<IFormFile> files)
    {
        _ossService.PutData(files);
    }
}
