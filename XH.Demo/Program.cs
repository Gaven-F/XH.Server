// This file is auto-generated, don't edit it. Thanks.

using Tea;




namespace AlibabaCloud.SDK.Sample
{
    public class Sample
    {

        /**
         * 使用 Token 初始化账号Client
         * @return Client
         * @throws Exception
         */
        public static AlibabaCloud.SDK.Dingtalkoauth2_1_0.Client CreateClient()
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config();
            config.Protocol = "https";
            config.RegionId = "central";
            return new AlibabaCloud.SDK.Dingtalkoauth2_1_0.Client(config);
        }

        public static void Main(string[] args)
        {
            AlibabaCloud.SDK.Dingtalkoauth2_1_0.Client client = CreateClient();
            AlibabaCloud.SDK.Dingtalkoauth2_1_0.Models.GetAccessTokenRequest getAccessTokenRequest = new AlibabaCloud.SDK.Dingtalkoauth2_1_0.Models.GetAccessTokenRequest
            {
                AppKey = "dingifzl2bbmtmajc48t",
                AppSecret = "7r8UJvYuLl81pmksnak--ssRvzesG9H4nc7PydTOfOMxShFDS1w1R7qvZFOrUYoz",
            };
            try
            {
                var res = client.GetAccessToken(getAccessTokenRequest);
                Console.WriteLine(res.Body.AccessToken);
            }
            catch (TeaException err)
            {
                if (!AlibabaCloud.TeaUtil.Common.Empty(err.Code) && !AlibabaCloud.TeaUtil.Common.Empty(err.Message))
                {
                    // err 中含有 code 和 message 属性，可帮助开发定位问题
                    Console.WriteLine(err.Message);
                }
            }
            catch (Exception _err)
            {
                TeaException err = new TeaException(new Dictionary<string, object>
                {
                    { "message", _err.Message }
                });
                if (!AlibabaCloud.TeaUtil.Common.Empty(err.Code) && !AlibabaCloud.TeaUtil.Common.Empty(err.Message))
                {
                    Console.WriteLine(err.Message);
                    // err 中含有 code 和 message 属性，可帮助开发定位问题
                }
            }
        }


    }
}