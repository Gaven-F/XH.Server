namespace Server.Application.Entities;

public class ET_C : BasicEntity
{
#pragma warning disable IDE1006
    public string T_CorpId { get; set; } = string.Empty;

    public string T_ApplicantDepart { get; set; } = string.Empty;

    public string T_TopicTitle { get; set; } = string.Empty;

    public string T_TopicContent { get; set; } = string.Empty;

    public string T_ReviewConference { get; set; } = string.Empty;

    public string T_MeetingTime { get; set; } = string.Empty;

    public string T_Participants { get; set; } = string.Empty;

    public string T_Annex { get; set; } = string.Empty;

    public string T_Notes { get; set; } = string.Empty;

    public string C_ContractNumber_A { get; set; } = string.Empty;

    public DateTime C_SignDate_A { get; set; } = DateTime.Now;

    public string C_OurCompany_A { get; set; } = string.Empty;

    public string C_OppositeCompany_A { get; set; } = string.Empty;

    public string C_Content_A { get; set; } = string.Empty;

    public string C_Picture_A { get; set; } = string.Empty;

    public string C_ApprovalAnnex_A { get; set; } = string.Empty;

    public string C_ContractNumber_B { get; set; } = string.Empty;

    public DateTime C_SignDate_B { get; set; } = DateTime.Now;

    public string C_OurCompany_B { get; set; } = string.Empty;

    public string C_OppositeCompany_B { get; set; } = string.Empty;

    public string C_Content_B { get; set; } = string.Empty;

    public string C_Picture_B { get; set; } = string.Empty;

    public string C_ApprovalAnnex_B { get; set; } = string.Empty;

    public string AssociateOtherId { get; set; } = string.Empty;

#pragma warning restore IDE1006
}
