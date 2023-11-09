using Neivce_admin.Services;

namespace Neivce_admin
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();

			DependencyService.Register<IAdminService, AdminService>();
		}
	}
}