using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using NPOI.XWPF.UserModel;
using Server.Entry.Utils;
using Utils;

namespace Server.Web.Controllers;

[Route("[controller]/[action]")]
[ApiDescriptionSettings(Order = 99)]
public class DemoController(BaseFunc dingtalk) : IDynamicApiController
{
    public ActionResult Index()
    {
        var userInfo =
           dingtalk
           .GetAllUserInfo(["职位", "姓名"])
           .ToList();

        return new OkObjectResult(userInfo);
    }

    public ActionResult GetAllUserInfo(
        [FromServices] global::Utils.BaseFunc dingtalk,
        [FromQuery] List<string>? fileds
    )
    {
        var data = dingtalk.GetAllUserInfo(fileds ?? []);
        return new OkObjectResult(data);
    }

    [HttpGet]
    public ActionResult ReturnDocx()
    {
        using var doc = new XWPFDocument();
        doc.CreateParagraph();

        var sw = new NPOIStream() { AllowClose = false };
        sw.AllowClose = false;
        doc.Write(sw);
        sw.Flush();
        sw.Seek(0, SeekOrigin.Begin);
        sw.AllowClose = true;
        return new FileStreamResult(
            sw,
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
        );
    }
}
