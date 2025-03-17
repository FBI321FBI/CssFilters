using System.Reflection;
using CssFilters.CommandManager;

namespace CssFilters.GroupManager.Extensions
{
	public static class FilterManagerExtension
	{
		public static GroupManager UseGroupManager(this FilterManager filterManager, Assembly assembly)
		{
			var groupManeger = new GroupManager(filterManager);
			filterManager.FilterManagers.Add(groupManeger);
			groupManeger.ChangeOptions(o =>
			{
				o.PluginAssembly = assembly;
				o.SetPlugin(filterManager.OptionsBase.Plugin);
				o.FilterLogger = filterManager.OptionsBase.FilterLogger;
			});
			groupManeger.AutoSearchGroups();
			return groupManeger;
		}
	}
}
