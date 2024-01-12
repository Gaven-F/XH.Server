using System.Diagnostics;
using System.Text.Json;
using Tea;

public static class DingtalkUtils
{
	public static string AppKey { get; } = "dingifzl2bbmtmajc48t";
	public static string AppSecret { get; } = "7r8UJvYuLl81pmksnak--ssRvzesG9H4nc7PydTOfOMxShFDS1w1R7qvZFOrUYoz";
	public static string Agentid { get; } = "2791318037";

	private readonly static string _ERROR = "ERROR";
	private readonly static string _GET_WORK_USER_ID_LIST_URL = "https://oapi.dingtalk.com/topapi/smartwork/hrm/employee/queryonjob";
	private readonly static string _GET_USER_INFO_URL = "https://oapi.dingtalk.com/topapi/smartwork/hrm/employee/v2/list";

	private static AlibabaCloud.SDK.Dingtalkoauth2_1_0.Client GetClient()
	{

		var config = new AlibabaCloud.OpenApiClient.Models.Config();
		config.Protocol = "https";
		config.RegionId = "central";

		return new AlibabaCloud.SDK.Dingtalkoauth2_1_0.Client(config);
	}

	private static DingTalk.Api.DefaultDingTalkClient GetV2Client(string url)
	{
		return new DingTalk.Api.DefaultDingTalkClient(url);
	}

	public static string GetToken()
	{
		var client = GetClient();
		var request = new AlibabaCloud.SDK.Dingtalkoauth2_1_0.Models.GetAccessTokenRequest
		{
			AppKey = AppKey,
			AppSecret = AppSecret
		};
		try
		{
			var response = client.GetAccessToken(request);
			Trace.WriteLine(JsonSerializer.Serialize(response));
			return response.Body.AccessToken;
		}
		catch (TeaException _err)
		{
			if (!AlibabaCloud.TeaUtil.Common.Empty(_err.Code) && !AlibabaCloud.TeaUtil.Common.Empty(_err.Message))
			{
				System.Diagnostics.Debug.WriteLine(_err.Message);
				Console.WriteLine(_err.Message);
			}
			throw;
		}
		catch (Exception _err)
		{
			var err = new TeaException(new Dictionary<string, object>
				{
					{ "message", _err.Message }
				});

			if (!AlibabaCloud.TeaUtil.Common.Empty(err.Code) && !AlibabaCloud.TeaUtil.Common.Empty(err.Message))
			{
				Console.WriteLine(err.Message);
			}
		}
		return _ERROR;
	}

	public static IEnumerable<string>? GetUserIds()
	{
		var client = GetV2Client(_GET_WORK_USER_ID_LIST_URL);
		var req = new DingTalk.Api.Request.OapiSmartworkHrmEmployeeQueryonjobRequest
		{
			StatusList = "2,3,5,-1",
			Offset = 0L,
			Size = 50L,
		};
		var res = client.Execute(req, GetToken());
		if (res.IsError)
		{
			Debug.WriteLine(JsonSerializer.Serialize(res));
			//throw new Exception("返回值错误！");
			var bg = Console.BackgroundColor;
			Console.BackgroundColor = ConsoleColor.Red;
			var fg = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.DarkGreen;

			Console.WriteLine("DingTalk Error");
			Console.WriteLine(res.Errmsg ?? res.ErrMsg);

			Console.BackgroundColor = bg;
			Console.ForegroundColor = fg;

		}
		else
		{
			var jsonRes = JsonDocument.Parse(res.Body).RootElement;
			Trace.WriteLine(res.Body);
			return jsonRes.GetProperty("result").GetProperty("data_list").EnumerateArray().Select(x => x.GetString() ?? "");
		}

		return null;
	}

	public static IEnumerable<string>? GetUserInfo(IEnumerable<string> userId, IEnumerable<string>? fileds)
	{

		var client = GetV2Client(_GET_USER_INFO_URL);
		var req = new DingTalk.Api.Request.OapiSmartworkHrmEmployeeV2ListRequest
		{
			Agentid = Convert.ToInt64(Agentid),
			UseridList = userId.Aggregate((l, c) => $"{l},{c}")
		};

		if (fileds != null)
		{
			req.FieldFilterList = fileds.Aggregate((l, c) => $"{l},{c}");
		}

		var res = client.Execute(req, GetToken());

		if (!res.IsError)
		{
			var jsonRes = JsonDocument.Parse(res.Body).RootElement;
			return jsonRes.GetProperty("result").EnumerateArray().Select(it => it.GetRawText());
		}
		else
		{
			Debug.WriteLine(JsonSerializer.Serialize(res));
			//throw new Exception("返回值错误！");
			var bg = Console.BackgroundColor;
			Console.BackgroundColor = ConsoleColor.Red;
			var fg = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.DarkGreen;

			Console.WriteLine("DingTalk Error");
			Console.WriteLine(res.Errmsg ?? res.ErrMsg);

			Console.BackgroundColor = bg;
			Console.ForegroundColor = fg;

		}

		return null;
	}

	public static void SendMsg(IEnumerable<string> userId, string msg)
	{
		var client = GetV2Client("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2");
		var req = new DingTalk.Api.Request.OapiMessageCorpconversationAsyncsendV2Request
		{
			AgentId = Convert.ToInt64(Agentid),
			ToAllUser = false,
			Msg_ = new DingTalk.Api.Request.OapiMessageCorpconversationAsyncsendV2Request.MsgDomain()
			{
				Msgtype = "text",
				Text = new DingTalk.Api.Request.OapiMessageCorpconversationAsyncsendV2Request.TextDomain()
				{
					Content = msg
				}
			},
			UseridList = userId.Aggregate((l, c) => $"{l},{c}"),
		};
		var res = client.Execute(req, GetToken());
		if (res.IsError)
		{
			Debug.WriteLine(JsonSerializer.Serialize(res));
			//throw new Exception("返回值错误！");
			var bg = Console.BackgroundColor;
			Console.BackgroundColor = ConsoleColor.Red;
			var fg = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("DingTalk Error");
            Console.WriteLine(res.Errmsg ?? res.ErrMsg);

			Console.BackgroundColor = bg;
			Console.ForegroundColor = fg;
        }
	}
}


