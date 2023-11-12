using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using XH.Application.Ali;
using XH.Application.System.Services;
using XH.Core.Database.Tables;
using XH.Core.DataBase;

namespace XH.Web.Core.Controllers;

/// <summary>
/// 测试服务接口
/// </summary>
public class DemoController : IDynamicApiController
{
    private readonly ISystemService _systemService;
    private readonly DTService _dTService;
    private readonly OSSService _ossService;

    public DemoController(ISystemService systemService, DTService dTContext, OSSService ossService)
    {
        _systemService = systemService;
        _dTService = dTContext;
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
        return _dTService.GetUserInfo(code);
    }

    /// <summary>
    /// 提交文件
    /// </summary>
    /// <param name="files"></param>
    public List<string> PostFile([FromForm] List<IFormFile> files)
    {
        return _ossService.PutData(files);
    }

    /// <summary>
    /// 提交请假请求
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="businessTrip"></param>
    public void PostBusinessTrip([FromServices] Repository<BusinessTrip> repository, FBusinessTrip businessTrip)
    {
        repository.InsertReturnSnowflakeId(businessTrip.Adapt<BusinessTrip>());
    }

    /// <summary>
    /// 获取出差信息
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="userId">用户id，不填写或填写ALL默认获取所有</param>
    /// <returns></returns>
    public IEnumerable<FBusinessTrip> GetBusinessTrip([FromServices] Repository<BusinessTrip> repository,string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
            .Equals("all") || it.CorpId.Equals(userId))
            .Select(it => it.Adapt<FBusinessTrip>());
    }
}
