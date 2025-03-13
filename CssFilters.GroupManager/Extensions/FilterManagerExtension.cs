using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CssFilters.GroupManager.Extensions
{
	public static class FilterManagerExtension
	{
		public static GroupManager UseGroupManager(this FilterManager filterManager)
		{
			var goupManeger = new GroupManager(filterManager);
			filterManager.FilterManagers.Add(goupManeger);
			return goupManeger;
		}
	}
}
