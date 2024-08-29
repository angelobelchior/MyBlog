using System.Reflection;
using Microsoft.OpenApi.Models;
using MyBlog.Backend.Counters;
using MyBlog.Backend.Endpoints;
using MyBlog.Backend.Extensions;
using MyBlog.Shared.Extensions;

using Scalar.AspNetCore;

var assemblyName = Assembly.GetExecutingAssembly().GetName();
var appName = assemblyName.Name!;
var appVersion = assemblyName.Version!.ToString();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSingleton<MyBlogCounters>();

builder.Services.AddLog(configuration, appName, appVersion);
builder.Services.AddAllHealthChecks(configuration);
builder.Services.AddCache(configuration);
builder.Services.AddRepositories(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var info = new OpenApiInfo()
    {
        Title = "MyBlog - API Documentation",
        Version = "v1",
        Description = "API Documentation for MyBlog",
    };
    
    c.SwaggerDoc("v1", info);
    var xmlFile = "MyBlog.Shared.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseLog();
app.UseHealthCheckEndpoints();

app.UseSwagger(options =>
{
    options.RouteTemplate = "openapi/{documentName}.json";
});

app.MapScalarApiReference();

app.UseHttpsRedirection();
app.UsePostEndpoints();
app.Run();