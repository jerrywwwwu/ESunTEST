using VoteOnline.Models;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using NLog.Web;
using Serilog;
using Serilog.Events;

//Log.Logger = new LoggerConfiguration()
//	//Serilog�n�g�J���̧C���Ŭ�Information
//	.MinimumLevel.Information()
//	//Microsoft.AspNetCore�}�Y�����O������warning
//	.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
//	//�glog��Logs��Ƨ���log.txt�ɮפ��A�åB�H�Ѭ���찵�ɮפ���
//	.WriteTo.File("./Logs/logs.txt", rollingInterval: RollingInterval.Day)
//	.CreateLogger();
//Log.Information("Starting web host");

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.
builder.Services.AddControllersWithViews ();
builder.Services.AddScoped<RepositoryAdapter>();
builder.Services.AddDbContext<VoteOnlineContext> (options =>
options.UseSqlServer (builder.Configuration.GetConnectionString ("VoteOnlineConnectstring")));

// 2. �ϥ� Serilog ���N���ت� Logger
//builder.Host.UseSerilog();

// �M���w�] logger provider�]�Ҧp console�^
builder.Logging.ClearProviders();

// �]�w�̧C log ����
builder.Logging.SetMinimumLevel(LogLevel.Trace);

// �ϥ� NLog ���� logging provider�]���J nlog.config�^
builder.Host.UseNLog();
builder.Services.AddLogging(logging =>
{
	//�M���쥻�� logging provider
	logging.ClearProviders();
	//�]�w logging �� minmum level �� trace
	logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
	//�ϥ� NLog �@�� logging provider
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
