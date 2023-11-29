using Neivce_admin.Models;
using Neivce_admin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Neivce_admin.ViewModels
{
	public class AddUpdateStudentPageViewModel : BaseViewModel
	{
		#region Properties
		private readonly IAdminService _AdminService;
		private AdminModel _AdminDetail = new AdminModel();
		private string getsheetid;

		public AdminModel AdminDetail
		{
			get => _AdminDetail;
			set => SetProperty(ref _AdminDetail, value);
		}
		#endregion

		#region Constructor
		public AddUpdateStudentPageViewModel(string getSheet)
		{
			_AdminService = DependencyService.Resolve<IAdminService>();
		}

		public AddUpdateStudentPageViewModel(AdminModel adminResponse, string getSheet)
		{
			getsheetid = getSheet;
			_AdminService = DependencyService.Resolve<IAdminService>();
			AdminDetail = new AdminModel
			{
				StudentID = adminResponse.StudentID,
				EnglishName = adminResponse.EnglishName,
				week1 = adminResponse.week1,
				week2 = adminResponse.week2,
				week3 = adminResponse.week3,
				week4 = adminResponse.week4,
				week5 = adminResponse.week5,
				week6 = adminResponse.week6,
				week7 = adminResponse.week7,
				week8 = adminResponse.week8,
				week9 = adminResponse.week9,
				week10 = adminResponse.week10,
				week11 = adminResponse.week11,
				week12 = adminResponse.week12,
				week13 = adminResponse.week13,
				week14 = adminResponse.week14,
			};
		}
		#endregion

		#region Commands
		public ICommand SaveStudentCommand => new Command(async () =>
		{
			if (IsBusy) return;
			IsBusy = true;
			bool res = await _AdminService.AddOrUpdateAdmin(AdminDetail, getsheetid);
			if (res)
			{
				AdminModel adminDetail = AdminDetail;
				if (!string.IsNullOrWhiteSpace(adminDetail.StudentID))
				{
					await App.Current.MainPage.DisplayAlert("Success!", "Record Updated successfully.", "Ok");
				}
				else
				{
					AdminDetail = new AdminModel() { };
					await App.Current.MainPage.DisplayAlert("Success!", "Record added successfully.", "Ok");
				}
			}
			IsBusy = false;
		});
		#endregion
	}
}
