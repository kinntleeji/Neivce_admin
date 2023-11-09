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
				week = f.Object.week
			}).ToList();
		}
	}
}
