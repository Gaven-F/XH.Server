using Microsoft.AspNetCore.Mvc;

namespace Server.Entry.Controllers;

[Route("[controller]/[action]")]
public class DemoController
{
    public IActionResult Index()
    {
        return new OkObjectResult(new { Id = "100", Name = "GavenF" });
    }
}
