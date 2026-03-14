using Microsoft.AspNetCore.Http.Connections;
using Vektorel.RealTimeMessages.Hubs;

namespace Vektorel.RealTimeMessages
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();
			builder.Services.AddSignalR()
							.AddJsonProtocol(config =>
							{
								config.PayloadSerializerOptions.PropertyNamingPolicy = null;
							});

			var app = builder.Build();

			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapHub<HotelHub>("/hub", options =>
			{
				options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
			});

			app.Run();
		}
	}
}
