using Microsoft.Extensions.Logging;
using Neivce_admin.Services;

namespace Neivce_admin
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
		builder.Logging.AddDebug();
			builder.Services.AddSingleton<IAdminService, AdminService>();
#endif

			return builder.Build();
		}
	}
}