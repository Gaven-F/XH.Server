using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    //.AddJsonOptions(option =>
    //{
    //    option.JsonSerializerOptions.PropertyNamingPolicy = null;
    //    option.JsonSerializerOptions.DictionaryKeyPolicy = null;
    //})
    .AddOData(option =>
    {
        option.Select().Count().Filter().OrderBy().SetMaxTop(10);
    })
    .AddODataNewtonsoftJson()
    .AddNewtonsoftJson(option =>
    {
        option.SerializerSettings.Converters.Add(new StringEnumConverter());
        option.SerializerSettings.Converters.Add(new JavaScriptDateTimeConverter());
        option.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
        option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    })
    ;


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

app
    .UseODataRouteDebug()
    .UseODataQueryRequest();

app.MapControllers();

app.Run();
