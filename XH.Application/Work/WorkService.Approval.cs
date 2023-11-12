using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XH.Core.Database.Tables.ApprovalTables;
using XH.Core.DataBase.Tables;
using XH.Core.Interfaces.AsTools;

namespace XH.Application.Work;
public partial class WorkService : IApproveService
{
	public void Approve(long approvalId, ApprovalStatusEnum status)
	{
		throw new NotImplementedException();
	}

	public void ApproveBack(long approvalId, string msg = "无")
	{
		throw new NotImplementedException();
	}

	public void ApprovePass(long approvalId, string msg = "无")
	{
		throw new NotImplementedException();
	}

	public void BindApprocal<T>() where T : BaseEntry, INeedApprove, new()
	{
		throw new NotImplementedException();
	}

	public void CreateApprovalConfig()
	{
		throw new NotImplementedException();
	}

	public void CreateApprovalStepByConfig(string config)
	{
		throw new NotImplementedException();
	}

	public (bool isApproved, string msg) IsApproved(long entryId)
	{
		throw new NotImplementedException();
	}
}
