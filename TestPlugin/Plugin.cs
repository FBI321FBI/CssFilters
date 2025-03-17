using System.Reflection;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CssFilters;
using CssFilters.Attributes;
using CssFilters.CommandManager.Extensions;
using CssFilters.CommandManager.Models;
using CssFilters.Enums;
using CssFilters.GroupManager.Attributes;
using CssFilters.GroupManager.Extensions;

namespace TestPlugin
{
	public class Plugin : BasePlugin
	{
		#region Data
		#region Override
		public override string ModuleName => "";

		public override string ModuleVersion => "";
		#endregion
		private FilterManager _filterManager;
		#endregion

		#region .ctor
		public Plugin()
		{

		}
		#endregion

		#region Override
		public override void Load(bool hotReload)
		{
			FilterManager filterManager = new FilterManager();
			var filterCommandManager = filterManager.UseFilterManager(this);
			var groupManager = filterManager.UseGroupManager(Assembly.GetExecutingAssembly());

			filterCommandManager.UseFilterCommandManager()
				.AddCommandWithFilters("css_test", "description", new TestCommand1().Handler)
				.AddGroup("TestFilters");
		}
		#endregion
	}

	public class TestCommand1
	{
		public void Handler(CCSPlayerController? player, CommandInfo info)
		{
			Console.WriteLine("Основаня команда выполнена успешно.");
		}
	}

	[GroupName("TestFilters")]
	[FilterName("ФИЛЬТР НОМЕР АДИН")]
	public class Filter1 : FilterCommandBase
	{
		public override void Execute(CCSPlayerController? player, CommandInfo info)
		{
			if (player is null)
			{
				SetFilterResult(FilterResults.Next);
			}
		}
	}

	[GroupName("TestFilters")]
	public class Filter2 : FilterCommandBase
	{
		public override void Execute(CCSPlayerController? player, CommandInfo info)
		{
			
		}
	}
}
