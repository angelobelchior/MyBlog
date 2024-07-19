using MyBlog.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyBlog.Repositories;

var builder = WebApplication.CreateBuilder(args);

var sqlServerConnectionString = builder.Configuration.GetConnectionString("SqlServerConnectionString")!;
var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnectionString")!;

builder.Services
    .AddHealthChecks()
    .AddSqlServer(name: "SqlServer", connectionString: sqlServerConnectionString, tags: ["database"], healthQuery: "exec proc @abc", failureStatus: HealthStatus.Degraded)
    .AddRedis(name: "Redis", redisConnectionString: redisConnectionString, tags: ["cache"])
    ;

builder.Services.AddRazorPages();

builder.Services.AddDbContext<MyBlogDbContext>(options =>
    options.UseSqlServer(sqlServerConnectionString));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnectionString;
    options.InstanceName = "MyBlogInstance";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.UseAllHealthChecks();

app.Run();