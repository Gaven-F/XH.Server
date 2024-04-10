using XH_Server.Domain.Basic;

namespace XH_Server.Application.Entities;

public class EBind : BasicEntity
{
    public string EquipmentId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}
