using System.Reflection;
using MyBlog.Backend.Endpoints;
using MyBlog.Backend.Extensions;
using MyBlog.Shared.Extensions;

var assemblyName = Assembly.GetExecutingAssembly().GetName();
var appName = assemblyName.Name!;
var appVersion = assemblyName.Version!.ToString();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddLog(configuration, appName, appVersion);
builder.Services.AddAllHealthChecks(configuration);
builder.Services.AddRepositories(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseLog();
app.UseHealthCheckEndpoints();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UsePostEndpoints();
app.Run();