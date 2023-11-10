using System;
using System.Collections.Generic;
using AlibabaCloud.SDK.Dingtalkoauth2_1_0;
using AlibabaCloud.SDK.Dingtalkoauth2_1_0.Models;
using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using Furion.Logging;
using Tea;

namespace XH.Core.DT;
public class DTContext
{
    private static string _APP_KEY = "dingifzl2bbmtmajc48t";
    private static string _APP_SECRET = "7r8UJvYuLl81pmksnak--ssRvzesG9H4nc7PydTOfOMxShFDS1w1R7qvZFOrUYoz";

    /**
       * 使用 Token 初始化账号Client
       * @return Client
       * @throws Exception
       */
    private static Client CreateClient()
    {
        var config = new AlibabaCloud.OpenApiClient.Models.Config
        {
            Protocol = "https",
            RegionId = "central",
        };
        return new Client(config);
    }

    private static string GetAccessToken()
    {
        // TODO:
        // Token的缓存时间为2h，尝试进行TOKEN有效时间判断，防止每次在有效时间内进行重复获取Token

        var token = "ERROR";
        var client = CreateClient();
        var getAccessToken = new GetAccessTokenRequest()
        {
            AppKey = _APP_KEY,
            AppSecret = _APP_SECRET
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

    private static string GetUserInfo()
    {
        var client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/v2/user/getuserinfo");
        var req = new OapiV2UserGetuserinfoRequest();
        var res = client.Execute(req, GetAccessToken());

        return res.Body;
    }
}
