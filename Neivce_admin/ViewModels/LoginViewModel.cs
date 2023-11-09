using Neivce_admin.Services;
using Neivce_admin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neivce_admin.ViewModels
{
	public class LoginViewModel
	{
		private INavigation _navigation;
		private string englishName;
		private string passwordT;

		public event PropertyChangedEventHandler PropertyChanged;

		public Command LoginBtn { get; }
		public Command Picker { get; }

		public string EnglishName
		{
			get => englishName; set
			{
				englishName = value;
				RaisePropertyChanged("EnglishName");
			}
		}

		public string PasswordT
		{
			get => passwordT; set
			{
				passwordT = value;
				RaisePropertyChanged("PasswordT");
			}
		}

		private string _selected;
		public string selected
		{
			get { return _selected; }
			set
			{
				_selected = value;
				RaisePropertyChanged();
			}
		}

		public LoginViewModel(INavigation navigation)
		{
			this._navigation = navigation;
			var subcodeList = new List<string>();
			subcodeList.Add("Sheet1");
			subcodeList.Add("Sheet2");
			subcodeList.Add("Sheet3");
			subcodeList.Add("Sheet4");
			subcodeList.Add("Sheet5");
			subcodeList.Add("Sheet6");
			subcodeList.Add("Admin");
			Picker suCopicker = new Picker { Title = "Select Program Subject Code" };
			suCopicker.ItemsSource = subcodeList;
			Label monkeyNameLabel = new Label();
			monkeyNameLabel.SetBinding(Label.TextProperty, new Binding("SelectedItem", source: suCopicker));

			LoginBtn = new Command(LoginBtnTappedAsync);
			Picker = new Command(OnPickerSelectedIndexChanged);
		}

		public async void OnPickerSelectedIndexChanged()
		{

			await App.Current.MainPage.DisplayAlert("Alert", selected, "OK");

		}

		private async void LoginBtnTappedAsync(object obj)
		{
			if (string.IsNullOrEmpty(EnglishName) || string.IsNullOrEmpty(PasswordT))
				await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Teacher Name and Password", "OK");
			else
			{
				var user = await AdminUpLoServices.GetUser(englishName, selected);
				if (user != null)
					if (EnglishName == user.EnglishName && PasswordT == user.StudentID && EnglishName == user.TeacherName)
					{
						await App.Current.MainPage.DisplayAlert("Login Success", "Welcome " +  EnglishName + " Subject Code:" + selected, "Ok");
						await App.Current.MainPage.Navigation.PushAsync(new Dashboard(EnglishName, PasswordT, selected));
					}
					else if (EnglishName == user.EnglishName && PasswordT == user.StudentID && PasswordT.Contains("Admin") || PasswordT.Contains("admin"))
					{
						await App.Current.MainPage.DisplayAlert("Login Success", "Welcome Admin " + EnglishName + " Subject Code:" + selected, "Ok");
						await App.Current.MainPage.Navigation.PushAsync(new Dashboard(EnglishName, PasswordT, selected));
					}
					else
					{
						await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Teacher Name and Password", "OK");
					}
				else 
				{
					await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
				}	
			}
		}

		private void RaisePropertyChanged(string v = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
		}
	}
}
