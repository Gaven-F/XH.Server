using System.Text.Json.Serialization;
using Masuit.Tools.Mime;
using Server.Core.Config;
using Server.Domain.Converters;
using Server.Entry.Utils;

MimeMapper.MimeTypes[".xlsx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

var builder = WebApplication.CreateBuilder(args).Inject();

builder.Services.AddControllers().AddInject();

builder.Services.AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	options.JsonSerializerOptions.Converters.Add(new JsonLongToStringConverter());
	options.JsonSerializerOptions.IncludeFields = true;
});

builder.Services.AddCorsAccessor();

// 应用服务及其依赖服务注入
builder
	.Services
	// 核心服务
	.AddSingleton(typeof(ConfigService))
	.AddScoped(typeof(OStorageService))
	.AddScoped(typeof(DatabaseService))
	.AddScoped(typeof(ApprovedPolicyService))
	.AddSingleton(typeof(Utils.BaseFunc))
	// 伪·领域服务
	.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>))
	.AddScoped(typeof(IBasicEntityService<>), typeof(BasicEntityService<>));

//// 应用服务
//.AddScoped(typeof(BasicApplicationApi<>));

var app = builder.Build();

app.UseMiddleware<GUtilsMiddleware>();

app.UseRouting();

app.UseAuthentication().UseAuthorization();

app.UseInject().UseCorsAccessor();

app.MapControllers();

app.Run();
