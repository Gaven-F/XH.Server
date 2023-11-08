using Furion.DynamicApiController;
using XH.Application.System.Services;

namespace XH.Web.Core.Controllers;

/// <summary>
/// 系统服务接口
/// </summary>
public class SystemController : IDynamicApiController
{
    private readonly ISystemService _systemService;
    public SystemController(ISystemService systemService)
    {
        _systemService = systemService;
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
}
