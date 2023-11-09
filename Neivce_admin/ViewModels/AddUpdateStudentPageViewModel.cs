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
		private string getweek;

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
			getsheetid = getSheet;
		}

		public AddUpdateStudentPageViewModel(AdminModel adminResponse, string getSheet)
		{
			
			_AdminService = DependencyService.Resolve<IAdminService>();
			AdminDetail = new AdminModel
			{
				StudentID = adminResponse.StudentID,
				EnglishName = adminResponse.EnglishName,
				week = adminResponse.week
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
				if (!string.IsNullOrWhiteSpace(adminDetail.Key))
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
