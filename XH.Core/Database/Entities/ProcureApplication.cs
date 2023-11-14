using SqlSugar;
using System;
using System.Collections.Generic;

namespace XH.Core.DataBase.Entities;

public class ProcureApplication : BaseEntity
{
    public string? CorpId { get; set; }
    public string? ProcureProject { get; set; }
    public string? ProcurType { get; set; }
    public string? ExplanatExpenditure { get; set; }
    public DateTimeOffset PurchaseDate { get; set; }
    public string? TotalAmount { get; set; }
    public string? Notes { get; set; }
    [SugarColumn(IsJson = true)]
    public List<string>? Picture { get; set; }
    [SugarColumn(IsJson = true)]
    public List<string>? Annex { get; set; }
    public string? ProcureMethod { get; set; }
}
