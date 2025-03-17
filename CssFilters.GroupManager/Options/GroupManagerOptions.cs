using System.Reflection;
using CounterStrikeSharp.API.Core;
using CssFilters.Options;

namespace CssFilters.GroupManager.Options
{
	public class GroupManagerOptions : OptionsBase
	{
		#region Data
		internal Assembly? pluginAssembly;
		#endregion


		#region Properties
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
		#endregion

		#region .ctor
		public GroupManagerOptions(BasePlugin plugin) : base(plugin)
		{
		}
		#endregion
	}
}
