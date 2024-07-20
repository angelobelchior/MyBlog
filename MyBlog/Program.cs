using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyBlog;
using MyBlog.Repositories;

var builder = WebApplication.CreateBuilder(args);

var sqlServerConnectionString = builder.Configuration.GetConnectionString("SqlServerConnectionString")!;
var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnectionString")!;

builder.Services
    .AddHealthChecks()
    .AddSqlServer(name: "SqlServer", connectionString: sqlServerConnectionString, tags: ["database"],
        healthQuery: "exec proc @abc", failureStatus: HealthStatus.Degraded)
    .AddRedis(name: "Redis", redisConnectionString: redisConnectionString, tags: ["cache"])
    ;

builder.Services.AddRazorPages();
builder.Services.AddStackExchangeRedisOutputCache(options =>
{
    options.Configuration = redisConnectionString;
    options.InstanceName = "MyBlogInstance";
});
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(p => p.Expire(TimeSpan.FromSeconds(10)));
});

builder.Services.AddDbContext<MyBlogDbContext>(options =>
    options.UseSqlServer(sqlServerConnectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseOutputCache();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.UseAllHealthChecks();

app.Run();