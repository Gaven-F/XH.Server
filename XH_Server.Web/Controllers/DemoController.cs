using Microsoft.AspNetCore.Mvc;
using XH_Server.Application.Entities;

namespace XH_Server.Web.Controllers;

[NonController]
[Route("[controller]/[action]")]
public class DemoController
{
	public IResult Index()
	{
		return Results.Json(new ETopic() { Id = 31231L });
	}
}
