using Furion.DynamicApiController;
using Utils;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 耗材
/// </summary>


public class ConsumableManagement(BaseFunc DingTalk)
	: BasicApplicationApi<EConsumableManagement>,
		IDynamicApiController
{
	//public ActionResult PostFile(string coperId, string spaceId, string fileId)
	//{
	//	var unioId = DingTalk.GetUserUid(coperId);
	//}

}
