using Furion.DynamicApiController;
using Masuit.Tools.Mime;
using Masuit.Tools.Systems;
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
		[FromServices] BaseFunc dingtalk,
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

	[HttpPost]
	public ActionResult UploadFile(IFormFile file)
	{
		return new OkObjectResult(new
		{
			file.FileName,
			file.Length,
			file.ContentType
		});
	}

	public async Task<ActionResult> DemoApi()
	{
		// Authorization
		// x-oss-date
		var (url, authorization, date) = dingtalk.GetFileDownload("ii7zUuiiddyYJJYtE0n8iSShAiEiE", "20917246345", "137773722348");
		//return new OkObjectResult(dingtalk.GetUserUid("01052133605426110345"));
		var client = new HttpClient();
		client.DefaultRequestHeaders.Add("Authorization", authorization);
		client.DefaultRequestHeaders.Add("x-oss-date", date);

		var res = await client.GetAsync(url);
		var contentType = res.Content.Headers.ContentType?.MediaType ?? "ERROR";
		contentType = contentType == "ERROR" ? "ERROR" : MimeMapper.ExtTypes[contentType];
		var fileName = new SnowFlakeNew(12).GetUniqueShortId() + contentType;
		if (res.Content.Headers.ContentDisposition != null)
		{
			fileName = res.Content.Headers.ContentDisposition.FileName ?? fileName;
		}
		var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		using var file = File.Create(Path.Combine(desktop, fileName));
		(await res.Content.ReadAsStreamAsync()).CopyTo(file);
		file.Close();

		return new OkResult();

	}
}
