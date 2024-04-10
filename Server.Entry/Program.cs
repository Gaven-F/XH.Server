using IGeekFan.AspNetCore.Knife4jUI;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    //.AddJsonOptions(option =>
    //{
    //    option.JsonSerializerOptions.PropertyNamingPolicy = null;
    //    option.JsonSerializerOptions.DictionaryKeyPolicy = null;
    //})
    .AddNewtonsoftJson(option =>
    {
        option.SerializerSettings.Converters.Add(new StringEnumConverter());
        option.SerializerSettings.Converters.Add(new JavaScriptDateTimeConverter());
    });

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
