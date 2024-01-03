using XH_Server.Application;
using XH_Server.Core.Config;
using XH_Server.Core.Database;
using XH_Server.Core.Dingtalk;
using XH_Server.Domain.Approve;
using XH_Server.Domain.Basic;
using XH_Server.Domain.Repository;

var builder = WebApplication.CreateBuilder(args).Inject();

builder.Services
	.AddControllers()
	.AddInject();

builder.Services.AddCorsAccessor();

// 应用服务及其依赖服务注入
builder.Services
	// 核心服务
	.AddScoped(typeof(ConfigService))
	.AddScoped(typeof(DingtalkService))
	.AddScoped(typeof(OStorageService))
	.AddScoped(typeof(DatabaseService))
	.AddSingleton(typeof(DingtalkService))
	// 伪·领域服务
	.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>))
	.AddScoped(typeof(IBasicEntityService<>), typeof(BasicEntityService<>))
	.AddScoped(typeof(IApproveService), typeof(ApprovalService))
	// 应用服务
	.AddScoped(typeof(IBasicApplicationService<>), typeof(BasicApplicationService<>));

var app = builder.Build();

app.UseAuthentication().UseAuthorization();

app.UseInject().UseCorsAccessor();
app.MapControllers();

app.Run();
