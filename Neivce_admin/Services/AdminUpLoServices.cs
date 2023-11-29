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
	public class AdminUpLoServices
	{
		public static FirebaseClient firebase = new FirebaseClient("https://attendance-neivce-default-rtdb.firebaseio.com/");

		//Read All    
		public static async Task<List<AdminModel>> GetAllUser(string selected)
		{
			try
			{
				var userlist = (await firebase
				.Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").Child(selected)
				.OnceAsync<AdminModel>()).Select(item =>
				new AdminModel
				{
					EnglishName = item.Object.EnglishName,
					StudentID = item.Object.StudentID,
					TeacherName = item.Object.TeacherName,
					Key = item.Object.Key,
				}).ToList();
				return userlist;
			}
			catch (Exception e)
			{
				Debug.WriteLine($"Error:{e}");
				return null;
			}
		}

		//Read     
		public static async Task<AdminModel> GetUser(string englishName, string selected)
		{
			try
			{
				var allUsers = await GetAllUser(selected);
				await firebase
				.Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").Child(selected)
				.OnceAsync<AdminModel>();
				return allUsers.Where(a => a.EnglishName == englishName).FirstOrDefault();
			}
			catch (Exception e)
			{
				Debug.WriteLine($"Error:{e}");
				return null;
			}
		}
		public static async Task Updatecode(string englishName, string getsheet, string getCode)
		{
			var toUpdatePerson = (await firebase
			  .Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").Child(getsheet)
			  .OnceAsync<AdminModel>()).Where(a => a.Object.TeacherName == englishName).ToList();

			foreach (var person in toUpdatePerson)
			{
				person.Object.Code = getCode;

				await firebase
				  .Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").Child(getsheet)
				  .Child(person.Key)
				  .PutAsync(person.Object);
			}
		}
		public static async Task<List<AdminModel>> Getsample(string barsheet)
		{
			if(barsheet == null)
			{
				return null;
			}

			return (await firebase
				.Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").Child(barsheet).OnceAsync<AdminModel>()).Select(item =>
				new AdminModel
				{
					StudentID = item.Object.StudentID,
					EnglishName = item.Object.EnglishName,
					week1 = item.Object.week1,
					week2 = item.Object.week2,
					week3 = item.Object.week3,
					week4 = item.Object.week4,
					week5 = item.Object.week5,
					week6 = item.Object.week6,
					week7 = item.Object.week7,
					week8 = item.Object.week8,
					week9 = item.Object.week9,
					week10 = item.Object.week10,
					week11 = item.Object.week11,
					week12 = item.Object.week12,
					week13 = item.Object.week13,
					week14 = item.Object.week14,
				}).ToList();
		}
		public static async Task writebar(string englishName, string getsheet, string getCode)
		{
			var toUpdatePerson = (await firebase
			  .Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").Child(getsheet)
			  .OnceAsync<AdminModel>()).Where(a => a.Object.EnglishName == englishName).ToList();

			foreach (var person in toUpdatePerson)
			{

				person.Object.Bar = getCode;

				await firebase
				  .Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").Child(getsheet)
				  .Child(person.Key)
				  .PutAsync(person.Object);
			}
		}
		public static async Task<List<FirebaseObject<AdminModel>>> Getdataconv()
		{
			return (await firebase.Child("1KldxaMhsbRc1B9Hr6Z7Hw2pJldXTAZT3u-C3h-iCywE").OnceAsync<AdminModel>()).ToList();
		}
	}
}
