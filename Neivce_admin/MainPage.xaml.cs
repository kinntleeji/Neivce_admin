using Neivce_admin.Models;
using Neivce_admin.Services;
using Neivce_admin.ViewModels;
using Neivce_admin.Views;
using Newtonsoft.Json;

namespace Neivce_admin
{
	public partial class MainPage : ContentPage
	{

		LoginViewModel loginViewModel;
		private bool isToggled = false;
		List<string> subjlist = new List<string>();

		public MainPage()
		{
			InitializeComponent();
			loginViewModel = new LoginViewModel(Navigation);
			BindingContext = loginViewModel;
		}

		protected override async void OnAppearing()
		{
			var data = await AdminUpLoServices.Getdataconv();
			var json = JsonConvert.SerializeObject(data);
			List<AdminModel> firebaseItems = JsonConvert.DeserializeObject<List<AdminModel>>(json);
			foreach (var item in firebaseItems)
			{
				string gettest = item.Key;
				subjlist.Add(gettest);
			}
			subjpicker.ItemsSource = subjlist;
		}

		private void subjPickerSubjectgetIndexChanged(object sender, EventArgs e)
		{
			loginViewModel.subjPickerSubjectgetIndexChanged();
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