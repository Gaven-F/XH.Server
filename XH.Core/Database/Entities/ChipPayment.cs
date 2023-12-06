using System.Collections.Generic;


namespace XH.Core.DataBase.Entities;

public class ChipPayment : BaseEntity
{
	public string? CorpId { get; set; }
	public string? ContractNumber { get; set; }
	public string? ReasonPayment { get; set; }
	public string? DatePayment { get; set; }
	public string? PaymentMethods { get; set; }
	public string? ReceivingUnit { get; set; }
	public string? Bank { get; set; }
	public string? Account { get; set; }
	public string? RateSelection { get; set; }
	public string? AmountOne { get; set; }
	public string? Unit { get; set; }
	public string? AmountTwo { get; set; }
	public string? TotalAmount { get; set; }
	public string? PaymentType { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? ProofPayment { get; set; }
	public string? Advances { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? ContractOrder { get; set; }
	public string? Remark { get; set; }
	public string? State { get; set; }


}
