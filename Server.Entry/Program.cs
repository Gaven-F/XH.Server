using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Builder;
using Server.Entry;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddOpenApiDocument();

var app = builder.Build();

app.UseRouting();

app.UseOpenApi()
    .UseKnife4UI(options =>
    {
        options.RoutePrefix = "";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 Doc");
    })
    .UseSwaggerUi();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
