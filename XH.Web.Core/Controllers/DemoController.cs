using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
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
	/// 提交出差请求
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

    /// <summary>
    /// 提交请假请求
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="leave"></param>
    public void PostLeave([FromServices] Repository<Leave> repository, Leave leave)
    {
        repository.InsertReturnSnowflakeId(leave);
    }

    /// <summary>
    /// 提交采购申请
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="procureApplication"></param>
    public void PostProcureApplication([FromServices] Repository<ProcureApplication> repository, ProcureApplication procureApplication)
    {
        repository.InsertReturnSnowflakeId(procureApplication);
    }

    /// <summary>
    /// 提交采购确认
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="procurementConfirmation"></param>
    public void PostProcurementConfirmation([FromServices] Repository<ProcurementConfirmation> repository, ProcurementConfirmation procurementConfirmation)
    {
        repository.InsertReturnSnowflakeId(procurementConfirmation);
    }

    /// <summary>
    /// 获取请假信息
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="userId">用户id，不填写或填写ALL默认获取所有</param>
    /// <returns></returns>
    public IEnumerable<LeaveVo> GetLeave([FromServices] Repository<Leave> repository, string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
            .Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<LeaveVo>>();
    }

    /// <summary>
    /// 获取采购申请信息
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="userId">用户id，不填写或填写ALL默认获取所有</param>
    /// <returns></returns>
    public IEnumerable<ProcureApplicationVo> GetProcureApplication([FromServices] Repository<ProcureApplication> repository, string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
            .Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<ProcureApplicationVo>>();
    }

    /// <summary>
    /// 获取采购确认信息
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="userId">用户id，不填写或填写ALL默认获取所有</param>
    /// <returns></returns>
    public IEnumerable<ProcurementConfirmationVo> GetProcurementConfirmation([FromServices] Repository<ProcurementConfirmation> repository, string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
            .Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<ProcurementConfirmationVo>>();
    }

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="id">数据id</param>
    /// <param name="status">状态</param>
    /// <param name="table">表名</param>
    /// <returns></returns>
    public int StatusChange(string id, int status, string table)
    {
        var data = _db.Queryable<BaseEntity>().AS(table).Single(it => it.Id == id.Adapt<long>());

        if (data != null)
        {
            data.Status = status;
        }
        else
        {
            throw new System.Exception("未找到数据！");
        }

        return _db.Updateable(data).AS(table).ExecuteCommand();
    }

    public int DelData(string id, string table)
    {
        var data = _db.Queryable<BaseEntity>().AS(table).Single(it => it.Id == id.Adapt<long>());

        if (data != null)
        {
            data.IsDelete = true;
            data.UpdateTime = DateTimeOffset.Now;
        }
        else
        {
            throw new System.Exception("未找到数据！");
        }

        return _db.Updateable(data).AS(table).ExecuteCommand();
    }

    /// <summary>
    /// 提交会议
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="data"></param>
    public void PostMeeting([FromServices] Repository<Meeting> repository, Meeting data)
    {
        repository.InsertReturnSnowflakeId(data);
    }

    /// <summary>
    /// 获取会议信息
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="userId">用户id，不填写或填写ALL默认获取所有</param>
    /// <returns></returns>
    public IEnumerable<MeetingVo> GetMeeting([FromServices] Repository<Meeting> repository, string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
            .Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<MeetingVo>>();
    }

    /// <summary>
    /// 提交印章
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="data"></param>
    public void PostSeal([FromServices] Repository<Seal> repository, Seal data)
    {
        repository.InsertReturnSnowflakeId(data);
    }

    /// <summary>
    /// 获取印章信息
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="userId">用户id，不填写或填写ALL默认获取所有</param>
    /// <returns></returns>
    public IEnumerable<SealVo> GetSeal([FromServices] Repository<Seal> repository, string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
            .Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<SealVo>>();
    }

    /// <summary>
    /// 提交合同
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="data"></param>
    public void PostContract([FromServices] Repository<Contract> repository, Contract data)
    {
        repository.InsertReturnSnowflakeId(data);
    }

    /// <summary>
    /// 获取合同信息
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="userId">用户id，不填写或填写ALL默认获取所有</param>
    /// <returns></returns>
    public IEnumerable<ContractVo> GetContract([FromServices] Repository<Contract> repository, string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
            .Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<ContractVo>>();
    }
    /// <summary>
	/// 提交会议记录
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="data"></param>
	public void PostMeetingLog([FromServices] Repository<MeetingLog> repository, MeetingLog data)
	{
		repository.InsertReturnSnowflakeId(data);
	}

	/// <summary>
	/// 获取会议记录信息
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="userId">用户id，不填写或填写ALL默认获取所有</param>
	/// <returns></returns>
	public IEnumerable<MeetingLogVo> GetMeetingLog([FromServices] Repository<MeetingLog> repository, string userId = "ALL")
	{
		return repository.GetList((it) => userId.ToLower()
			.Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<MeetingLogVo>>();
	}

	/// <summary>
	/// 提交流片付款
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="data"></param>
	public void PostChipPayment([FromServices] Repository<ChipPayment> repository, ChipPayment data)
	{
		repository.InsertReturnSnowflakeId(data);
	}

	/// <summary>
	/// 获取流片付款信息
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="userId">用户id，不填写或填写ALL默认获取所有</param>
	/// <returns></returns>
	public IEnumerable<ChipPaymentVo> GetChipPayment([FromServices] Repository<ChipPayment> repository, string userId = "ALL")
	{
		return repository.GetList((it) => userId.ToLower()
			.Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<ChipPaymentVo>>();
	}

	// 项目管理
	/// <summary>
	/// 提交项目管理
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="data"></param>
	public void PostProjectManagement([FromServices] Repository<ProjectManagement> repository, ProjectManagement data)
	{
		repository.InsertReturnSnowflakeId(data);
	}

	/// <summary>
	/// 获取项目管理信息
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="userId"></param>
	/// <returns></returns>
	public IEnumerable<ProjectManagementVo> GetProjectManagement([FromServices] Repository<ProjectManagement> repository, string userId = "ALL")
	{
		return repository.GetList((it) => userId.ToLower()
			.Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<ProjectManagementVo>>();
	}

	// 报销
	/// <summary>
	/// 提交报销
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="data"></param>
	public void PostReimbursement([FromServices] Repository<Reimbursement> repository, Reimbursement data)
	{
		repository.InsertReturnSnowflakeId(data);
	}

	/// <summary>
	/// 获取报销信息
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="userId"></param>
			/// <returns></returns>
	public IEnumerable<ReimbursementVo> GetReimbursement([FromServices] Repository<Reimbursement> repository, string userId = "ALL")
	{
		return repository.GetList((it) => userId.ToLower()
			.Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<ReimbursementVo>>();
	}

	// 发票
	/// <summary>
	/// 提交发票
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="data"></param>
	public void PostInvoicing([FromServices] Repository<Invoicing> repository, Invoicing data)
	{
		repository.InsertReturnSnowflakeId(data);
	}

	/// <summary>
	/// 获取发票信息
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="userId"></param>
	/// <returns></returns>
	public IEnumerable<InvoicingVo> GetInvoicing([FromServices] Repository<Invoicing> repository, string userId = "ALL")
	{
		return repository.GetList((it) => userId.ToLower()
			.Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<InvoicingVo>>();
	}

	// 收款
	/// <summary>
	/// 提交收款
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="data"></param>
	public void PostIssueReceipts([FromServices] Repository<IssueReceipts> repository, IssueReceipts data)
	{
		repository.InsertReturnSnowflakeId(data);
	}

	/// <summary>
	/// 获取收款信息
	/// </summary>
	/// <param name="repository"></param>
	/// <param name="userId"></param>
	/// <returns></returns>
	public IEnumerable<IssueReceiptsVo> GetIssueReceipts([FromServices] Repository<IssueReceipts> repository, string userId = "ALL")
	{
		return repository.GetList((it) => userId.ToLower()
			.Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<IssueReceiptsVo>>();
	}

    /// <summary>
    /// 提交主题
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="data"></param>
    public void PostTopic([FromServices] Repository<Topic> repository, Topic data)
    {
        repository.InsertReturnSnowflakeId(data);
    }

    /// <summary>
    /// 获取主题信息
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="userId">用户id，不填写或填写ALL默认获取所有</param>
    /// <returns></returns>
    public IEnumerable<TopicVo> GetTopic([FromServices] Repository<Topic> repository, string userId = "ALL")
    {
        return repository.GetList((it) => userId.ToLower()
            .Equals("all") || it.CorpId!.Equals(userId)).Adapt<List<TopicVo>>();
    }

	[HttpGet("{id}")]
	public string GetUserInfoById([FromServices] DTService dt, string id)
	{
		return dt.GetUserInfoById(id);
	}
}
