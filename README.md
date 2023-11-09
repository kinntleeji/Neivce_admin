# Neivce_admin

change 
BarList.xaml update

<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="Neivce_admin.Views.BarList"
             Title="BarList">
    <VerticalStackLayout>
        <StackLayout>
            <ListView x:Name="infolistview" ItemsSource="{Binding infos}">
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <ViewCell>
                            <StackLayout Orientation="Horizontal" Padding="5">
                                <StackLayout HorizontalOptions="StartAndExpand">
                                    <Label Text="{Binding EnglishName}" FontSize="Small"/>
                                    <Label Text="{Binding StudentID}" />
                                    <Label Text="{Binding TeacherName}" />
                                    <Label Text="{Binding Sheet}" />
                                </StackLayout>
                            </StackLayout>
                        </ViewCell>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
        </StackLayout>
    </VerticalStackLayout>
</ContentPage>

BarList.xaml.cs update

using Neivce_admin.Models;
using Neivce_admin.Services;

namespace Neivce_admin.Views;

public partial class BarList : ContentPage
{
	private string barsheet;
	public BarList(string getSheet)
	{
		InitializeComponent();
		barsheet = getSheet;
	}

	protected override async void OnAppearing()
	{
		var infos = await AdminUpLoServices.Getsample(barsheet);
		foreach (var info in infos)
		{
			var attend = 0;

			var week = WeekHandler.WeekList(info);


			foreach (var item in week)
			{
				attend += Convert.ToInt16(item == "" ? -1 :item);
			}
			

			if (attend > 0)
			{
				if (attend < 12)
				{
					await AdminUpLoServices.writebar(info.EnglishName, barsheet, "Barred");
					await AdminUpLoServices.writebar2(info.EnglishName, info.StudentID, info.TeacherName, barsheet);
				}
			}
			else
			{
				await AdminUpLoServices.writebar(info.EnglishName, barsheet, "");
			}
		}

		//RREADDDDD
		var infob = await AdminUpLoServices.writesample();
		infolistview.ItemsSource = infob;
	}
}

Dashboard.xaml
 <StackLayout IsVisible="false">
	<Picker x:Name="myPicker" Title="Select a monkey"/>
	<Label x:Name="lbljson" />
	<Button BackgroundColor="#292BE8" Text="JSON" Clicked="jsonOnClicked" CornerRadius="20" />
    </StackLayout>
</VerticalStackLayout>

Dashboard.xaml.cs
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
			//lbljson.Text += item.Key;
			string gettest = item.Key;
			List<string> stringList = new List<string> { gettest };
			myPicker.ItemsSource = stringList;
		}
	}
