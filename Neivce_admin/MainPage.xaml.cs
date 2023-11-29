using Neivce_admin.ViewModels;
using Neivce_admin.Views;

namespace Neivce_admin
{
	public partial class MainPage : ContentPage
	{

		LoginViewModel loginViewModel;
		private bool isToggled = false;

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
		private void OnsignshowClicked(object sender, EventArgs e)
		{
			isToggled = !isToggled;

			if (isToggled)
			{
				entrytest.IsPassword = false;
				imgbtnshow.Source = "shows.png";
			}

			else
			{
				entrytest.IsPassword = true;
				imgbtnshow.Source = "hides.png";
			}
		}
	}
}