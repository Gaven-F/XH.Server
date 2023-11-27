using XH.Core.Database.Entities;
using XH.Core.DataBase.Entities;

namespace XH.Web.Core.Controllers;

public class ProcureApplicationVo : ProcureApplication
{
	new public string Id { get; set; } = string.Empty;
}

public class ProcurementConfirmationVo : ProcurementConfirmation
{
	new public string Id { get; set; } = string.Empty;
}
public class LeaveVo : Leave
{
	new public string Id { get; set; } = string.Empty;
}
public class BusinessTripVo : BusinessTrip
{
	new public string Id { get; set; } = string.Empty;
}
public class MettingVo : Meeting
{
	new public string Id { get; set; } = string.Empty;
}
public class SealVo : Seal
{
	new public string Id { get; set; } = string.Empty;
}
public class ContractVo : Contract
{
	new public string Id { get; set; } = string.Empty;
}
public class TopicVo : Topic
{
	new public string Id { get; set; } = string.Empty;
}
public class MeetingLogVo : MeetingLog
{
	new public string Id { get; set; } = string.Empty;
}