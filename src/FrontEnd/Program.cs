using System.Reflection;
using MyBlog.Frontend.Extensions;
using MyBlog.Shared.Extensions;

var assemblyName = Assembly.GetExecutingAssembly().GetName();
var appName = assemblyName.Name!;
var appVersion = assemblyName.Version!.ToString();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddLog(configuration, appName, appVersion);
builder.Services.AddAllHealthChecks(configuration);
builder.Services.AddRazorPages();
builder.Services.AddRedisAddOutputCache(configuration);
builder.Services.AddBackend(configuration);

var app = builder.Build();
app.UseExceptionHandler("/Error");
app.UseHsts();
app.UseLog();
app.UseHealthCheckEndpoints();
app.UseOutputCache();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.UseLog();
app.Run();