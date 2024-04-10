using Mapster;
using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;

[AdaptTo("[name]Dto")]
public class ELeave : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string LeaveType { get; set; } = string.Empty;
    public string AnnualLeave { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SumTime { get; set; } = string.Empty;
    public string EndDateSuffix { get; set; } = string.Empty;
    public string StartDateSuffix { get; set; } = string.Empty;

    public string ReasonLeave { get; set; } = string.Empty;
    public string Annex { get; set; } = string.Empty;
}

