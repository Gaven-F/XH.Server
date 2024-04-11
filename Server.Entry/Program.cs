using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddOData(option =>
    {
        option.Select().Count().Filter().OrderBy().SetMaxTop(10);
    })
    .AddODataNewtonsoftJson()
    .AddNewtonsoftJson(options =>
    {
        // OData use camel case
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy { ProcessDictionaryKeys = true },
        };
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
        options.SerializerSettings.Converters = [
            new StringEnumConverter(),
            new JavaScriptDateTimeConverter(),
        ];
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

app
    .UseODataRouteDebug()
    .UseODataQueryRequest();

app.MapControllers();

app.Run();
