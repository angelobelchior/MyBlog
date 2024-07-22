using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyBlog;
using MyBlog.Repositories;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSerilog((_, options) =>
{
    //options.MinimumLevel.Debug();
    
    //options.WriteTo.Console(new CompactJsonFormatter());
    options.WriteTo.Console();

    //options.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day);

    options.Enrich.FromLogContext();
    options.Enrich.WithMemoryUsage();
    options.Enrich.WithThreadId();
    options.Enrich.WithProcessId();
    options.Enrich.WithProcessName();
    options.Enrich.WithEnvironmentUserName();
    options.Enrich.WithProperty("Application", "MyBlog");
    options.Enrich.WithProperty("Version", Assembly.GetExecutingAssembly().GetName().Version?.ToString());
    options.Enrich.WithProperty("Environment", builder.Environment.EnvironmentName);
    options.Enrich.WithExceptionDetails();
});

builder.Services.AddAllHealthChecks(configuration);
builder.Services.AddRazorPages();
builder.Services.AddRedisAddOutputCache(configuration);
builder.Services.AddDbContext<MyBlogDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("SqlServerConnectionString")!);
#if DEBUG
    options.EnableSensitiveDataLogging();
#endif
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseOutputCache();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.UseAllHealthChecks();

app.Run();