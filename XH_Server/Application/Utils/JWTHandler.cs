using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace XH_Server.Applications.Utils;

public class JWTHandler : AppAuthorizeHandler
{
    public override Task HandleAsync(AuthorizationHandlerContext context)
    {
        return Task.FromResult(true);
    }
}
