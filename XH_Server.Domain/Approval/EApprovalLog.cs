using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Approval;
public class EApprovalLog : BasicEntity
{
	public long DataId { get; set; }
	public long NodeId { get; set; }
	public string Idea { get; set; } = string.Empty;
	public ApprovalStatus Status { get; set; }
}

[Flags]
public enum ApprovalStatus
{
	Wait, Pass, Back
}
