namespace Server.Application.Entities;

public class EConsumableManagement : BasicEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public string Usage { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Count { get; set; }
    public double TotalPrice { get; set; }
    public string Remake { get; set; } = string.Empty;
}

public class EConsumableLog : BasicEntity
{
    public string CoperId { get; set; } = string.Empty;
    public string ConsumableId { get; set; } = string.Empty;
    public string ConsumableName { get; set; } = string.Empty;
    public string ConsumableLevel { get; set; } = string.Empty;
    public int Count { get; set; } = 0;
    public string Usage { get; set; } = string.Empty;
}
