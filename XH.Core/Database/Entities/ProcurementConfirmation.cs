using System;
using System.Collections.Generic;

namespace XH.Core.DataBase.Entities;

public class ProcurementConfirmation : BaseEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string? ProcurType { get; set; }
    public DateTimeOffset DeliveryTime { get; set; }
    public string? ProcurementDetails { get; set; }
    public string? ProcurName { get; set; }
    public string? PurchaseQuantity { get; set; }
    public string? Unit { get; set; }
    public string? UnitPrice { get; set; }
    public string? PaymentMethod { get; set; }
    public List<string>? ResultReport { get; set; }
    public List<string>? Annex { get; set; }

}