using System.Reflection;

namespace CssFilters.GroupManager.Extensions
{
	public static class FilterManagerExtension
	{
		public static GroupManager UseGroupManager(this FilterManager filterManager, Assembly assembly)
		{
			var groupManeger = new GroupManager(filterManager);
			groupManeger.ChangeOptions(o =>
			{
				o.PluginAssembly = assembly;
			});
			groupManeger.AutoSearchGroups();
			return groupManeger;
		}
	}
}
