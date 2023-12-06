using Microsoft.Extensions.DependencyInjection;
using XH_Server.Demo;
using XH_Server.Domain;
using XH_Server.Domain.Services.BasicEntityService;

var services = new ServiceCollection();

services.AddSingleton<ISqlSugarClient>(new SqlSugarClient(new ConnectionConfig
{
	ConnectionString = "dataSource=demo.sqlite",
	DbType = DbType.Sqlite
}));

services.AddScoped(typeof(IRepositoryService<>), typeof(Repository<>));
services.AddScoped(typeof(IBasicEntityService<>), typeof(BasicEntityService<>));

var data = new DemoEntity();

var servicesProvider = services.BuildServiceProvider();

servicesProvider.GetRequiredService<ISqlSugarClient>().DbMaintenance.CreateDatabase();
servicesProvider.GetRequiredService<ISqlSugarClient>().CodeFirst.InitTables(entityType: typeof(DemoEntity));


servicesProvider.GetRequiredService<IBasicEntityService<DemoEntity>>().CreateEntity(data);
