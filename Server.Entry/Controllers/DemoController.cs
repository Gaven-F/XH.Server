using Microsoft.AspNetCore.Mvc;

namespace Server.Entry.Controllers;

[Route("[controller]/[action]")]
public class DemoController
{
    public string Index()
    {
        return "Hello";
    }
}
