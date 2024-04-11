﻿using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using NPOI.XWPF.UserModel;
using Server.Application.Entities;
using Server.Web.Utils;

namespace Server.Web.Controllers;

[Route("[controller]/[action]")]
public class DemoController : IDynamicApiController
{
    public ActionResult Index()
    {
        return new OkObjectResult(new ETopic() { Id = 31231L });
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
        return new FileStreamResult(sw, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
    }

    public ActionResult GetAllUserInfo([FromServices] global::Utils.BaseFunc dingtalk, [FromQuery] List<string>? fileds)
    {
        var data = dingtalk.GetAllUsetInfo(fileds ?? []);
        return new OkObjectResult(data);
    }
}
