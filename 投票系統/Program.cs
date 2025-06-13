using VoteOnline.Models;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using NLog.Web;
using Serilog;
using Serilog.Events;

//Log.Logger = new LoggerConfiguration()
//	//Serilog要寫入的最低等級為Information
//	.MinimumLevel.Information()
//	//Microsoft.AspNetCore開頭的類別等極為warning
//	.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
//	//寫log到Logs資料夾的log.txt檔案中，並且以天為單位做檔案分割
//	.WriteTo.File("./Logs/logs.txt", rollingInterval: RollingInterval.Day)
//	.CreateLogger();
//Log.Information("Starting web host");

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.
builder.Services.AddControllersWithViews ();
builder.Services.AddScoped<RepositoryAdapter>();
builder.Services.AddDbContext<VoteOnlineContext> (options =>
options.UseSqlServer (builder.Configuration.GetConnectionString ("VoteOnlineConnectstring")));

// 2. 使用 Serilog 取代內建的 Logger
//builder.Host.UseSerilog();

// 清除預設 logger provider（例如 console）
builder.Logging.ClearProviders();

// 設定最低 log 等級
builder.Logging.SetMinimumLevel(LogLevel.Trace);

// 使用 NLog 做為 logging provider（載入 nlog.config）
builder.Host.UseNLog();
builder.Services.AddLogging(logging =>
{
	//清除原本的 logging provider
	logging.ClearProviders();
	//設定 logging 的 minmum level 為 trace
	logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
	//使用 NLog 作為 logging provider
	logging.AddNLog();
});

var app = builder.Build ();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment ()) {
	app.UseExceptionHandler ("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts ();
}

app.UseHttpsRedirection ();
app.UseStaticFiles ();

app.UseRouting ();

app.UseAuthorization ();

app.MapControllerRoute (
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run ();
