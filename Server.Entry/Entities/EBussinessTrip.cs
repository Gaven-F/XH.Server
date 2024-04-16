namespace Server.Application.Entities;

public class EBusinessTrip : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string TravelType { get; set; } = string.Empty;
    public string TypeDependent { get; set; } = string.Empty;
    public string PlaceDeparture { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string Vehicle { get; set; } = string.Empty;
    public string Colleague { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SumTime { get; set; } = string.Empty;
    public string BusinessTrip { get; set; } = string.Empty;
    public string Annex { get; set; } = string.Empty;
    public string EndDateSuffix { get; set; } = string.Empty;
    public string StartDateSuffix { get; set; } = string.Empty;
}
