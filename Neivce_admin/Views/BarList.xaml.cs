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
				}
			}
			else
			{
				await AdminUpLoServices.writebar(info.EnglishName, barsheet, "");
			}

		}

		//RREADDDDD

		infolistview.ItemsSource = infos;
	}
}