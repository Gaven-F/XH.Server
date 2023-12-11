using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace XH_Server.Web.Controllers;

public class Demo : IDynamicApiController
{
	[HttpGet]
	public string GetMsg()
	{
		return "Hello";
	}
}
