using System.Text.Json.Serialization;
using XH_Server.Core.Config;
using XH_Server.Core.Database;
using XH_Server.Domain.ApprocedPolicy;
using XH_Server.Domain.Basic;
using XH_Server.Domain.Converters;
using XH_Server.Domain.Repository;
using XH_Server.Web.Middlewares;


var builder = WebApplication.CreateBuilder(args).Inject();

builder.Services
	.AddControllers()
	.AddInject();

builder.Services.AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	options.JsonSerializerOptions.Converters.Add(new JsonLongToStringConverter());
});


builder.Services.AddCorsAccessor();

// 应用服务及其依赖服务注入
builder.Services
	// 核心服务
	.AddSingleton(typeof(ConfigService))
	.AddScoped(typeof(OStorageService))
	.AddScoped(typeof(DatabaseService))
	.AddScoped(typeof(ApprovedPolicyService))
	.AddSingleton(typeof(DingtalkUtils.DingtalkUtils))
	// 伪·领域服务
	.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>))
	.AddScoped(typeof(IBasicEntityService<>), typeof(BasicEntityService<>));
//// 应用服务
//.AddScoped(typeof(BasicApplicationApi<>));

var app = builder.Build();

app.UseMiddleware<GUtilsMiddleware>();

app.UseAuthentication().UseAuthorization();

app.UseInject().UseCorsAccessor();
app.MapControllers();

app.Run();
