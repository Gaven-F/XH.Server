using AlibabaCloud.SDK.Dingtalkoauth2_1_0;
using AlibabaCloud.SDK.Dingtalkoauth2_1_0.Models;
using DingTalk.Api;
using DingTalk.Api.Request;
using Furion.Logging;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Tea;

namespace XH.Application.Ali;
public class DTService
{
    private readonly string _appKey;
    private readonly string _appSecret;

    public DTService(IConfiguration conf)
    {
        var DTConfig = conf.GetSection("DTConfig");
        Console.WriteLine(DTConfig);
        _appKey = DTConfig.GetValue("AppKey", "")!;
        _appSecret = DTConfig.GetValue("AppSecret", "")!;
    }

    /**
       * 使用 Token 初始化账号Client
       * @return Client
       * @throws Exception
       */
    private Client CreateClient()
    {
        var config = new AlibabaCloud.OpenApiClient.Models.Config
        {
            Protocol = "https",
            RegionId = "central",
        };
        return new Client(config);
    }

    private string GetAccessToken()
    {
        // TODO:
        // Token的缓存时间为2h，尝试进行TOKEN有效时间判断，防止每次在有效时间内进行重复获取Token

        string token = "ERROR";
        var client = CreateClient();

        var getAccessToken = new GetAccessTokenRequest()
        {
            AppKey = _appKey,
            AppSecret = _appSecret
        };

        try
        {
            token = client.GetAccessToken(getAccessToken).Body.AccessToken;
        }
        catch (TeaException err)
        {
            Log.Warning(err.Message);
        }
        catch (Exception _err)
        {
            var err = new TeaException(new Dictionary<string, object>
            {{"message",_err.Message}});
            if (!AlibabaCloud.TeaUtil.Common.Empty(err.Code) && !AlibabaCloud.TeaUtil.Common.Empty(err.Message))
            {
                Log.Warning(err.Message);
            }
        }

        return token;
    }

    public string GetUserInfo(string code)
    {
        var client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/v2/user/getuserinfo");
        var req = new OapiV2UserGetuserinfoRequest()
        {
            Code = code
        };

        var res = client.Execute(req, GetAccessToken());
        var baseInfo = JsonDocument.Parse(res.Body).RootElement;
        if (baseInfo.GetProperty("errcode").GetInt32() == 0)
        {
            var infoClient = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/smartwork/hrm/employee/v2/list");
            var infoReq = new OapiSmartworkHrmEmployeeV2ListRequest();

            infoReq.Agentid = 2791318037;
            infoReq.UseridList = baseInfo.GetProperty("result").GetProperty("userid").GetString();

            var infoRes = infoClient.Execute(infoReq, GetAccessToken());

            return JsonSerializer.Serialize(new { baseInfo = res.Body, details = infoRes.Body });
        }
        else
        {
            return "ERROR";
        }

    }
    public string GetUserInfoById(string id)
    {
        var infoClient = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/smartwork/hrm/employee/v2/list");
        var infoReq = new OapiSmartworkHrmEmployeeV2ListRequest
        {
            Agentid = 2791318037,
            UseridList = id
        };

        var infoRes = infoClient.Execute(infoReq, GetAccessToken());
        return infoRes.Body;


    }
}
