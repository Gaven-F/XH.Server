using Furion.DynamicApiController;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 会议室
/// </summary>

public class MeetingRoomApplication
    : BasicApplicationApi<EMeetingRoomApplication>,
        IDynamicApiController { }
