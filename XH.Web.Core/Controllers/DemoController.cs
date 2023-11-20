using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using XH.Application.Ali;
using XH.Application.System.Services;
using XH.Core.Database.Entities;
using XH.Core.DataBase;
using XH.Core.DataBase.Entities;

namespace XH.Web.Core.Controllers;

/// <summary>
/// 测试服务接口
/// </summary>
public class DemoController : IDynamicApiController
{
    private readonly ISystemService _systemService;
    private readonly DTService _dTService;
    private readonly OSSService _ossService;
    private readonly ISqlSugarClient _db;

    public DemoController(ISystemService systemService, DTService dTContext, OSSService ossService, ISqlSugarClient db)
    {
        _systemService = systemService;
        _dTService = dTContext;
        _ossService = ossService;
        _db = db;
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
    public IEnumerable<BusinessTripVo> GetBusinessTrip([FromServices] Repository<BusinessTrip> repository, string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
            .Equals("all") || it.CorpId.Equals(userId)).Adapt<List<BusinessTripVo>>();
    }

    public void PostLeave([FromServices] Repository<Leave> repository, Leave leave)
    {
        repository.InsertReturnSnowflakeId(leave);
    }

    public void PostProcureApplication([FromServices] Repository<ProcureApplication> repository, ProcureApplication procureApplication)
    {
        repository.InsertReturnSnowflakeId(procureApplication);
    }

    public void PostProcurementConfirmation([FromServices] Repository<ProcurementConfirmation> repository, ProcurementConfirmation procurementConfirmation)
    {
        repository.InsertReturnSnowflakeId(procurementConfirmation);
    }


    public IEnumerable<LeaveVo> GetLeave([FromServices] Repository<Leave> repository, string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
                    .Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<LeaveVo>>();
    }

    public IEnumerable<ProcureApplicationVo> GetProcureApplication([FromServices] Repository<ProcureApplication> repository, string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
                    .Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<ProcureApplicationVo>>();
    }

    public IEnumerable<ProcurementConfirmationVo> GetProcurementConfirmation([FromServices] Repository<ProcurementConfirmation> repository, string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
                    .Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<ProcurementConfirmationVo>>();
    }

    public int StatusChange(string id, int status, string table)
    {
        var data = _db.Queryable<BaseEntity>().AS(table).Single(it => it.Id == id.Adapt<long>());

        if (data != null)
        {
            data.Status = status;
        } else
        {
            throw new System.Exception("未找到数据！");
        }

        return _db.Updateable(data).AS(table).ExecuteCommand();
    }
}
