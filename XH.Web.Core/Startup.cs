using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlSugar;
using XH.Application.Ali;
using XH.Core.DataBase;

namespace XH.Web.Core;

public class Startup : AppStartup
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddConsoleFormatter();
		services.AddJwt<JwtHandler>();

		services.AddCorsAccessor();

		services.AddControllers()
				.AddInjectWithUnifyResult();

		// 数据库服务
		services.AddSingleton<ISqlSugarClient>(DbContext.Instance)
				.AddScoped(typeof(Repository<>));

		// 阿里爸爸大套餐！
		services.AddSingleton(typeof(DTService));
		services.AddSingleton(typeof(OSSService));

	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		//app.UseHttpsRedirection();

		app.UseRouting();

		app.UseCorsAccessor();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseInject(string.Empty);

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}
}
