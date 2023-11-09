using Neivce_admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neivce_admin.Services
{
	public interface IAdminService
	{
		Task<bool> AddOrUpdateAdmin(AdminModel adminModel, string getsheetid);
		Task<bool> DeleteAdmin(string StudentID, string getsheetids);
		Task<List<AdminModel>> GetAllAdmin(string getsheetids);
	}
}
