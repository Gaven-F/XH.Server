using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;

public class EDeviceManagement : BasicEntity
{
    public string CorpId { get; set; } = string.Empty;
    public string BarcodeNumber { get; set; } = string.Empty;
    public string AttributionEngineer { get; set; } = string.Empty;
    public DateTime SampleProcessTime { get; set; }
    public string SampleHandler { get; set; } = string.Empty;
    public string DeviceUsageContent { get; set; } = string.Empty;
    public DateTime DeviceUsageTime { get; set; }
    public string EquipmentUsers { get; set; } = string.Empty;
    public string InternalDuration { get; set; } = string.Empty;
    public string ExternalDuration { get; set; } = string.Empty;
}

