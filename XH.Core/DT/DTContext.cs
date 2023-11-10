using System;
using System.Collections.Generic;
using AlibabaCloud.SDK.Dingtalkoauth2_1_0;
using AlibabaCloud.SDK.Dingtalkoauth2_1_0.Models;
using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using Furion.Logging;
using Microsoft.Extensions.Configuration;
using Tea;

namespace XH.Core.DT;
public class DTContext
{
    private readonly string _appKey;
    private readonly string _appSecret;

    public DTContext(IConfiguration conf)
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

        var token = "ERROR";
        var client = CreateClient();

        Log.Warning("key:{key},secret:{secret}",_appKey, _appSecret);
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
        Console.WriteLine(GetAccessToken());
        var res = client.Execute(req, GetAccessToken());

        return res.Body;
    }
}
