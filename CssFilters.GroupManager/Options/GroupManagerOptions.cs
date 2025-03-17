using System.Reflection;
using CssFilters.Options;

namespace CssFilters.GroupManager.Options
{
	public class GroupManagerOptions : OptionsBase
	{
		internal Assembly? pluginAssembly;
		internal Assembly PluginAssembly
		{
			get
			{
				if (pluginAssembly is null)
				{
					throw new ArgumentNullException(nameof(pluginAssembly) + " сборка плагина не задана.");
				}
				return pluginAssembly;
			}
			set
			{
				pluginAssembly = value;
			}
		}
	}
}
