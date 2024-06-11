using Furion.DatabaseAccessor;
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
        var userInfo = dingtalk.GetAllUserInfo(["职位", "姓名"]).ToList();

        return new OkObjectResult(userInfo);
    }

    public ActionResult GetAllUserInfo(
        [FromServices] BaseFunc dingtalk,
        [FromQuery] List<string>? fileds,
        [FromQuery] List<string>? userIds = null
    )
    {
        var data = dingtalk.GetAllUserInfo(fileds ?? [], userIds);
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

    [HttpPost]
    public ActionResult UploadFile(IFormFile file)
    {
        return new OkObjectResult(
            new
            {
                file.FileName,
                file.Length,
                file.ContentType
            }
        );
    }

    public async Task<ActionResult> DemoApi([FromServices] DatabaseService Db)
    {
        var data = await Db
            .Instance.Queryable<EConsumableManagement>()
            .GroupBy(it => new { it.Name })
            .Select(it => new
            {
                it.Name,
                it.Level,
                it.Remake,
                Count = SqlFunc.AggregateSumNoNull(it.Count)
            })
            .ToListAsync();

        return new OkObjectResult(data);
    }
}
