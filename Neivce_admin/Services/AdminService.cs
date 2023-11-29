using Firebase.Database;
using Firebase.Database.Query;
using Neivce_admin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neivce_admin.Services
{
	public class AdminService : IAdminService
	{
		FirebaseClient firebase = new FirebaseClient(Setting.FireBaseDatabaseUrl, new FirebaseOptions
		{
			AuthTokenAsyncFactory = () => Task.FromResult(Setting.FireBaseSeceret)
		});

		internal static Task GetUser(Func<string> toString)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> AddOrUpdateAdmin(AdminModel adminModel, string getsheetid)
		{
			if (!string.IsNullOrWhiteSpace(adminModel.StudentID))
			{
				try
				{
					await firebase.Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").Child(getsheetid).Child(adminModel.StudentID).PutAsync(adminModel);
					return true;
				}
				catch (Exception)
				{
					return false;
				}
			}
			else
			{
				var response = await firebase.Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").Child(getsheetid).PostAsync(adminModel);
				if (response.Key != null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}

		}
		public async Task<bool> DeleteAdmin(string StudentID, string getadsub)
		{
			try
			{
				await firebase.Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").Child(getadsub).Child(StudentID).DeleteAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<List<AdminModel>> GetAllAdmin(string getadsub)
		{
			return (await firebase.Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").Child(getadsub).OnceAsync<AdminModel>()).Select(f => new AdminModel
			{
				StudentID = f.Object.StudentID,
				EnglishName = f.Object.EnglishName,
				week1 = f.Object.week1,
				week2 = f.Object.week2,
				week3 = f.Object.week3,
				week4 = f.Object.week4,
				week5 = f.Object.week5,
				week6 = f.Object.week6,
				week7 = f.Object.week7,
				week8 = f.Object.week8,
				week9 = f.Object.week9,
				week10 = f.Object.week10,
				week11 = f.Object.week11,
				week12 = f.Object.week12,
				week13 = f.Object.week13,
				week14 = f.Object.week14,
				TeacherName = f.Object.TeacherName,
			}).ToList();
		}
	}
}
