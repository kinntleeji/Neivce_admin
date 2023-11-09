using Neivce_admin.ViewModels;
using Neivce_admin.Views;

namespace Neivce_admin
{
	public partial class MainPage : ContentPage
	{

		LoginViewModel loginViewModel;

		public MainPage()
		{
			InitializeComponent();
			loginViewModel = new LoginViewModel(Navigation);
			BindingContext = loginViewModel;
		}

		private void suCopicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			loginViewModel.OnPickerSelectedIndexChanged();
		}
	}
}