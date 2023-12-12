using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace XH_Server.Web.Controllers;

public class Demo : IDynamicApiController
{
    public string GetMsg()
    {
        return "Hello";
    }

    public string PostData([FromBody] string data = "")
    {
        Console.WriteLine(data);
        return data;
    }
}
