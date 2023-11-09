using Neivce_admin.Models;
using Neivce_admin.Services;
using Neivce_admin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Neivce_admin.ViewModels
{
	public class StudentListPageViewModel :BaseViewModel
	{
		#region Properties
		private bool _isRefreshing;
		private string getsheetids;
		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		private readonly IAdminService _AdminService;

		public ObservableCollection<AdminModel> Adming { get; set; } = new ObservableCollection<AdminModel>();
		#endregion

		#region Constructor
		public StudentListPageViewModel(string getSheet)
		{
			_AdminService = DependencyService.Resolve<IAdminService>();
			getsheetids = getSheet;
			GetAllAdmin(getsheetids);
		}

		#endregion
		#region Methods
		private void GetAllAdmin(string getsheetids)
		{
			IsBusy = true;
			Task.Run(async () =>
			{
				var AdminList = await _AdminService.GetAllAdmin(getsheetids);

				Device.BeginInvokeOnMainThread(() =>
				{

					Adming.Clear();
					if (AdminList?.Count > 0)
					{
						foreach (var adming in AdminList)
						{
							Adming.Add(adming);
						}
					}
					IsBusy = IsRefreshing = false;
				});

			});
		}
		#endregion

		#region Commands

		public ICommand RefreshCommand => new Command(() =>
		{
			IsRefreshing = true;
			GetAllAdmin(getsheetids);
		});

		public ICommand SelectedStudentCommand => new Command<AdminModel>(async (student) =>
		{
			if (student != null)
			{
				var response = await App.Current.MainPage.DisplayActionSheet("Options!", "Cancel", null, "Update Student", "Delete Student");

				if (response == "Update Student")
				{
					await App.Current.MainPage.Navigation.PushAsync(new AddUpdateStudent(student, getsheetids));
				}
				else if (response == "Delete Student")
				{
					IsBusy = true;
					bool deleteResponse = await _AdminService.DeleteAdmin(student.StudentID, getsheetids);
					if (deleteResponse)
					{
						GetAllAdmin(getsheetids);
					}
				}
			}
		});
		#endregion
	}
}
