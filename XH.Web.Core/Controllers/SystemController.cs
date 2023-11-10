using Furion.DynamicApiController;
using XH.Application.System.Services;
using XH.Core.DT;

namespace XH.Web.Core.Controllers;

/// <summary>
/// 系统服务接口
/// </summary>
public class SystemController : IDynamicApiController
{
    private readonly ISystemService _systemService;
    private readonly DTContext _dTContext;
    public SystemController(ISystemService systemService, DTContext dTContext)
    {
        _systemService = systemService;
        _dTContext = dTContext;
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
}
