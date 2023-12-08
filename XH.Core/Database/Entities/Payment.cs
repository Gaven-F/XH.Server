using System.Collections.Generic;
using XH.Core.DataBase.Entities;

namespace XH.Core.Database.Entities;
public class Payment : BaseEntity
{
	//string CorpId  申请人
	//string ReasonPayment   付款事由
	//string PaymentType 付款类型
	//string Amount  金额
	//string PaymentMethods  付款方式
	//string DatePayment 支付日期
	//string PurchaseDetails 采购明细
	//string ReceivingUnit   收款单位名称
	//string Bank    开户行
	//string Account 银行账号
	//string PaymentInstructions 付款说明
	//string SourcesFunding  明确列出经费来源
	//string Invoicing   供应商开票情况
	//stringlist  Picture 发票照片
	//stringlist Annex   付款合同扫描件
	//string Remark  备注
	public string? CorpId { get; set; }
	public string? ReasonPayment { get; set; }
	public string? PaymentType { get; set; }
	public string? Amount { get; set; }
	public string? PaymentMethods { get; set; }
	public string? DatePayment { get; set; }
	public string? PurchaseDetails { get; set; }
	public string? ReceivingUnit { get; set; }
	public string? Bank { get; set; }
	public string? Account { get; set; }
	public string? PaymentInstructions { get; set; }
	public string? SourcesFunding { get; set; }
	public string? Invoicing { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? Picture { get; set; }
	[SqlSugar.SugarColumn(IsJson = true, ColumnDataType = "JSON")]
	public List<string>? Annex { get; set; }
	public string? Remark { get; set; }

}
