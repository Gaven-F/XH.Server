using Furion.DynamicApiController;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 耗材
/// </summary>


public class ConsumableManagement
    : BasicApplicationApi<EConsumableManagement>,
        IDynamicApiController { }
