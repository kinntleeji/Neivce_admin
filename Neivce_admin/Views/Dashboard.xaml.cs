using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using Neivce_admin.Models;
using Neivce_admin.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text.Json.Serialization;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Neivce_admin.Views;

public partial class Dashboard : ContentPage
{
	private string getTeacherName;
	private string getSheet;
	private string getweek;
	private string getcode;
	private string getadsub;
	private Timer timer;
	private bool isActionExecuted = false;

	public Dashboard(string TeacherName, string PasswordT, string selected)
	{
		InitializeComponent();
		Random random = new Random();
		int value = random.Next(10000);
		getTeacherName = TeacherName;
		lbltecher.Text = getTeacherName;
		getSheet = selected;
		getcode = value.ToString();
		if (PasswordT.Contains("Admin") || PasswordT.Contains("admin"))
		{
			Page1.IsVisible = false;
			Page2.IsVisible = true; //t
		}
		else
		{
			Page1.IsVisible = true; //t
			Page2.IsVisible = false;
		}
		var weekList = new List<string>();
		weekList.Add("Select");
		weekList.Add("week1");
		weekList.Add("week2");
		weekList.Add("week3");
		weekList.Add("week4");
		weekList.Add("week5");
		weekList.Add("week6");
		weekList.Add("week7");
		weekList.Add("week8");
		weekList.Add("week9");
		weekList.Add("week10");
		weekList.Add("week11");
		weekList.Add("week12");
		weekList.Add("week13");
		weekList.Add("week14");
		Picker weekpick = new Picker { Title = "Select Week" };
		weekpick.ItemsSource = weekList;
		Label weekLabel = new Label();
		weekLabel.SetBinding(Label.TextProperty, new Binding("SelectedItem", source: weekpick));
		var adsuList = new List<string>();
		adsuList.Add("Sheet1");
		adsuList.Add("Sheet2");
		adsuList.Add("Sheet3");
		adsuList.Add("Sheet4");
		adsuList.Add("Sheet5");
		adsuList.Add("Sheet6");
		Picker adsupicker = new Picker { Title = "Select Subject" };
		adsupicker.ItemsSource = adsuList;
		Label adsuLabel = new Label();
		adsuLabel.SetBinding(Label.TextProperty, new Binding("SelectedItem", source: adsupicker));

		timer = new Timer(5 * 60 * 1000);
		timer.Elapsed += TimerElapsed;
		timer.AutoReset = false;
		timer.Enabled = true;
	}
	public async void OnPickerSubjectgetIndexChanged(object sender, EventArgs e)
	{
		var suCopicker = (Picker)sender;
		int selectedIndex = suCopicker.SelectedIndex;

		if (selectedIndex != -1)
		{
			getweek = (string)suCopicker.ItemsSource[selectedIndex];
			await DisplayAlert("Alert", getweek, "OK");
			lblcode.IsVisible = true;
			lblcode.Text = getcode + getweek;
			await AdminUpLoServices.Updatecode(getTeacherName, getSheet, getcode + getweek);
		}
	}
	public async void SuPickerSubjectgetIndexChanged(object sender, EventArgs e)
	{
		var suCopicker = (Picker)sender;
		int selectedIndex = suCopicker.SelectedIndex;

		if (selectedIndex != -1)
		{
			getadsub = (string)suCopicker.ItemsSource[selectedIndex];
			await DisplayAlert("Alert", getadsub, "OK");
		}
	}

	private void TimerElapsed(object sender, ElapsedEventArgs e)
	{
		Device.BeginInvokeOnMainThread(() =>
		{
			lblcode.Text = "5 minutes have passed.";
			AdminUpLoServices.Updatecode(getTeacherName, getSheet, getcode);
			Navigation.PushAsync(new MainPage());
		});
	}

	private void addStudent_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new AddUpdateStudent(getadsub));
	}

	private void showStudent_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new StudentList(getadsub));
	}
	private async void CopyOnClicked(object sender, EventArgs e)
	{
		await App.Current.MainPage.DisplayAlert("Alert", "Copy", "Ok");
		await Clipboard.SetTextAsync(getcode + getweek);
	}
	private async void logoutOnClicked(object sender, EventArgs e)
	{
		bool answer = await DisplayAlert("Logout?", "Would you like to Logout", "Yes", "No");
		if (answer)
		{
			await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
		}
		else
		{
			await App.Current.MainPage.DisplayAlert("Logout", "Thankyou", "Ok");
		}
	}
	private async void logoutWOnClicked(object sender, EventArgs e)
	{
		bool answer = await DisplayAlert("Logout?", "Would you like to Logout", "Yes", "No");
		if (answer)
		{
			await AdminUpLoServices.Updatecode(getTeacherName, getSheet, getcode);
			await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
		}
		else
		{
			await App.Current.MainPage.DisplayAlert("Logout", "Thankyou", "Ok");
		}
	}
	private async void BarPageOnClicked(object sender, EventArgs e)
	{
		await App.Current.MainPage.Navigation.PushAsync(new BarList(getadsub));
	}

	private async void jsonOnClicked(object sender, EventArgs e)
	{
		var data = await AdminUpLoServices.Getdataconv();
		var json = JsonConvert.SerializeObject(data);
		List<AdminModel> firebaseItems = JsonConvert.DeserializeObject<List<AdminModel>>(json);
		PopulateEditor(firebaseItems);
	}

	private void PopulateEditor(List<AdminModel> firebaseItems)
	{
		foreach (var item in firebaseItems)
		{
			DisplayAlert("Alert", item.Key, "OK");
			lbljson.Text += item.Key;
			string gettest = item.Key;
			List<string> stringList = new List<string> { gettest };
			myPicker.ItemsSource = stringList;
		}
	}
}