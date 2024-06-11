using System.Diagnostics;
using System.Text.Json;
using DingTalk.Api;
using DingTalk.Api.Request;
using Server.Core.Config;
using Server.Entry.Utils;
using Tea;
using Utils.Entity;

namespace Utils;

public class BaseFunc(ConfigService configService)
{
    public string AppKey { get; set; } =
        configService.DingTalkConfig.AppKey ?? "dingifzl2bbmtmajc48t";

    public string AppSecret { get; set; } =
        configService.DingTalkConfig.AppSecret
        ?? "7r8UJvYuLl81pmksnak--ssRvzesG9H4nc7PydTOfOMxShFDS1w1R7qvZFOrUYoz";

    public string AgentId { get; set; } = configService.DingTalkConfig.AgentId ?? "2791318037";

    private const string _GET_USER_INFO = "https://oapi.dingtalk.com/topapi/v2/user/getuserinfo";
    private const string _ERROR = "ERROR";
    private const string _GET_WORK_USER_ID_LIST_URL =
        "https://oapi.dingtalk.com/topapi/smartwork/hrm/employee/queryonjob";
    private const string _GET_USER_INFO_URL =
        "https://oapi.dingtalk.com/topapi/smartwork/hrm/employee/v2/list";
    private const string _GET_USER_GET = "https://oapi.dingtalk.com/topapi/v2/user/get";

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
        return new DefaultDingTalkClient(url);
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
            Trace.WriteLine(response.ToJsonString());
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
            Debug.WriteLine((res).ToJsonString());
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
                .Select(x => x.GetString() ?? string.Empty);
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
            Debug.WriteLine((res).ToJsonString());
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
            Debug.WriteLine((res).ToJsonString());
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
                Agentid = Convert.ToUInt32(AgentId),
                UseridList = baseInfo.GetProperty("result").GetProperty("userid").GetString()
            };

            var infoRes = infoClient.Execute(infoReq, GetToken());

            return (new { baseInfo = res.Body, details = infoRes.Body }).ToJsonString();
        }
        return _ERROR;
    }

    public IEnumerable<UserInfo> GetAllUserInfo(
        IEnumerable<string> fileds,
        IEnumerable<string>? userIds = null
    )
    {
        if (userIds == null)
        {
            userIds = GetUserIds();
        }
        //var userIds = GetUserIds();
        userIds.ThrowExpIfNull();

        return from info in GetUserInfo(userIds!, fileds)
            select UserInfo.FromJson(info);
    }

    public string GetUserUid(string coperId)
    {
        var client = GetV2Client(_GET_USER_GET);
        var req = new OapiUserGetRequest() { Userid = coperId };
        var res = client.Execute(req, GetToken());
        if (res.IsError)
            throw new Exception(nameof(GetUserIds), new(res.ErrMsg + res.Errmsg));
        var val = JsonDocument.Parse(res.Body).RootElement;
        if (val.TryGetProperty("result", out var result))
        {
            return result.GetProperty("unionid").GetString() ?? _ERROR;
        }
        return _ERROR;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="uId"></param>
    /// <param name="spaceId"></param>
    /// <param name="fileId"></param>
    /// <returns>
    /// ResourceUrls
    /// Authorization
    /// x-oss-date
    /// </returns>
    public (string url, string authorization, string date) GetFileDownload(
        string uId,
        string spaceId,
        string fileId
    )
    {
        var config = new AlibabaCloud.OpenApiClient.Models.Config
        {
            Protocol = "https",
            RegionId = "central"
        };
        var client = new AlibabaCloud.SDK.Dingtalkstorage_1_0.Client(config);

        var getFileDownloadInfoHeaders =
            new AlibabaCloud.SDK.Dingtalkstorage_1_0.Models.GetFileDownloadInfoHeaders()
            {
                XAcsDingtalkAccessToken = GetToken(),
            };
        var getFileDownloadInfoRequest =
            new AlibabaCloud.SDK.Dingtalkstorage_1_0.Models.GetFileDownloadInfoRequest
            {
                UnionId = uId,
            };

        var res = client.GetFileDownloadInfoWithOptions(
            spaceId,
            fileId,
            getFileDownloadInfoRequest,
            getFileDownloadInfoHeaders,
            new()
        );

        return (
            res.Body.HeaderSignatureInfo.ResourceUrls[0],
            res.Body.HeaderSignatureInfo.Headers["Authorization"],
            res.Body.HeaderSignatureInfo.Headers["x-oss-date"]
        );
    }
}
