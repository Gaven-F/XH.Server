using Furion.Schedule;
using XH_Server.Applications.Jobs;
using XH_Server.Applications.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Inject();
builder.GFInject();

builder.Services.AddJwt<JWTHandler>(enableGlobalAuthorize: true);

builder.Services.AddControllers();

builder.Services.AddInject();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseInject();

app.MapControllers();

app.Run();
