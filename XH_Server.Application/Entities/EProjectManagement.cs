using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;

public class EProjectManagement : BasicEntity
{
	public string CorpId { get; set; } = string.Empty;
	public string ItemNumber { get; set; } = string.Empty;
	public string ProjectName { get; set; } = string.Empty;
	public DateTime ProjectStartEndTime { get; set; }
	public string ResearchPersonnel { get; set; } = string.Empty;
	public string ProjectBudget { get; set; } = string.Empty;
	public string ProjectExecution { get; set; } = string.Empty;
	public string InitiationReport { get; set; } = string.Empty;
	public string AnnualReport { get; set; } = string.Empty;
	public string ClosingReport { get; set; } = string.Empty;
	public string Annex { get; set; } = string.Empty;
}

