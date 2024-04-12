using System.Diagnostics;
using System.Text.Json;
using DingTalk.Api;
using DingTalk.Api.Request;
using Server.Core;
using Server.Core.Config;
using Tea;
using Utils.Entity;

namespace Utils;

public class BaseFunc(ConfigService configService)
{
    public string AppKey { get; set; } =
        configService.DingtalkConfig.AppKey ?? "dingifzl2bbmtmajc48t";
    public string AppSecret { get; set; } =
        configService.DingtalkConfig.AppSecret
        ?? "7r8UJvYuLl81pmksnak--ssRvzesG9H4nc7PydTOfOMxShFDS1w1R7qvZFOrUYoz";
    public string AgentId { get; set; } = configService.DingtalkConfig.AgentId ?? "2791318037";

    private const string _GET_USER_INFO = "https://oapi.dingtalk.com/topapi/v2/user/getuserinfo";
    private const string _ERROR = "ERROR";
    private const string _GET_WORK_USER_ID_LIST_URL =
        "https://oapi.dingtalk.com/topapi/smartwork/hrm/employee/queryonjob";
    private const string _GET_USER_INFO_URL =
        "https://oapi.dingtalk.com/topapi/smartwork/hrm/employee/v2/list";
    private const uint _AGENTID = 2791318037;

    private static AlibabaCloud.SDK.Dingtalkoauth2_1_0.Client GetClient()
    {
        var config = new AlibabaCloud.OpenApiClient.Models.Config
        {
            Protocol = "https",
            RegionId = "central"
        };

        return new AlibabaCloud.SDK.Dingtalkoauth2_1_0.Client(config);
    }

    private static DefaultDingTalkClient GetV2Client(string url)
    {
        return new DingTalk.Api.DefaultDingTalkClient(url);
    }

    public string GetToken()
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
            if (
                !AlibabaCloud.TeaUtil.Common.Empty(_err.Code)
                && !AlibabaCloud.TeaUtil.Common.Empty(_err.Message)
            )
            {
                Debug.WriteLine(_err.Message);
                Console.WriteLine(_err.Message);
            }
            throw;
        }
        catch (Exception _err)
        {
            var err = new TeaException(
                new Dictionary<string, object> { { "message", _err.Message } }
            );

            if (
                !AlibabaCloud.TeaUtil.Common.Empty(err.Code)
                && !AlibabaCloud.TeaUtil.Common.Empty(err.Message)
            )
            {
                Console.WriteLine(err.Message);
            }
        }
        return _ERROR;
    }

    public IEnumerable<string>? GetUserIds()
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
            return jsonRes
                .GetProperty("result")
                .GetProperty("data_list")
                .EnumerateArray()
                .Select(x => x.GetString() ?? "");
        }

        return null;
    }

    public IEnumerable<string>? GetUserInfo(IEnumerable<string> userId, IEnumerable<string>? fileds)
    {
        var client = GetV2Client(_GET_USER_INFO_URL);
        var req = new OapiSmartworkHrmEmployeeV2ListRequest
        {
            Agentid = Convert.ToInt64(AgentId),
            UseridList = userId.Aggregate((l, c) => $"{l},{c}")
        };

        if (fileds != null && fileds.Any())
        {
            req.FieldFilterList = fileds!.Aggregate((l, c) => $"{l},{c}");
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

    public void SendMsg(IEnumerable<string> userId, string msg)
    {
        var client = GetV2Client(
            "https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2"
        );
        var req = new DingTalk.Api.Request.OapiMessageCorpconversationAsyncsendV2Request
        {
            AgentId = Convert.ToInt64(AgentId),
            ToAllUser = false,
            Msg_ =
                new DingTalk.Api.Request.OapiMessageCorpconversationAsyncsendV2Request.MsgDomain()
                {
                    Msgtype = "text",
                    Text =
                        new DingTalk.Api.Request.OapiMessageCorpconversationAsyncsendV2Request.TextDomain()
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

    public string GetUserInfo(string code)
    {
        var client = GetV2Client(_GET_USER_INFO);
        var req = new OapiV2UserGetuserinfoRequest() { Code = code };

        var res = client.Execute(req, GetToken());
        var baseInfo = JsonDocument.Parse(res.Body).RootElement;
        if (baseInfo.GetProperty("errcode").GetInt32() == 0)
        {
            var infoClient = new DefaultDingTalkClient(
                "https://oapi.dingtalk.com/topapi/smartwork/hrm/employee/v2/list"
            );
            var infoReq = new OapiSmartworkHrmEmployeeV2ListRequest
            {
                Agentid = _AGENTID,
                UseridList = baseInfo.GetProperty("result").GetProperty("userid").GetString()
            };

            var infoRes = infoClient.Execute(infoReq, GetToken());

            return JsonSerializer.Serialize(new { baseInfo = res.Body, details = infoRes.Body });
        }
        else
        {
            return _ERROR;
        }
    }

    public IEnumerable<UserInfo> GetAllUserInfo(IEnumerable<string> fileds)
    {
        var userIds = GetUserIds();
        userIds.ThrowExpIfNull();

        return from info in GetUserInfo(userIds!, fileds)
            select UserInfo.FromJson(info);
    }
}
