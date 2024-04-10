using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;
public class ET_C : BasicEntity
{
    public string T_CorpId { get; set; } = string.Empty;
    public string T_ApplicatDepart { get; set; } = string.Empty;
    public string T_TopicTitle { get; set; } = string.Empty;
    public string T_TopicContent { get; set; } = string.Empty;
    public string T_ReviewConference { get; set; } = string.Empty;
    public DateTime T_MeetingTime { get; set; }
    public string T_Participants { get; set; } = string.Empty;
    public string T_Annex { get; set; } = string.Empty;
    public string T_Notes { get; set; } = string.Empty;
    public string T_LawyerApproval { get; set; } = string.Empty;

    public string C_CorpId { get; set; } = string.Empty;
    public string C_ContractNumber { get; set; } = string.Empty;
    public string C_OurCompany { get; set; } = string.Empty;
    public string C_OppositeCompany { get; set; } = string.Empty;
    public string C_Content { get; set; } = string.Empty;
    public string C_Picture { get; set; } = string.Empty;
    public string C_AssociateOtherId { get; set; } = string.Empty;
    public string C_ApprovalAnnex { get; set; } = string.Empty;
    public DateTime C_SignDate { get; set; }
}
