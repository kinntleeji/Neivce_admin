using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neivce_admin.Models
{
	public class WeekHandler
	{
		public static List<string> WeekList(AdminModel o) 
		{
			return new List<string>()
			{
				o.week1,o.week2,o.week3,o.week4,o.week5,o.week6,o.week7,o.week8,o.week9,o.week10,o.week11, o.week12,o.week13,o.week14
			};
		}

	}
}
