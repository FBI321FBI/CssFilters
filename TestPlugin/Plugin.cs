using System.Reflection;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CssFilters;
using CssFilters.Attributes;
using CssFilters.AttrubuteCssHandler.Extensions;
using CssFilters.CommandManager.Extensions;
using CssFilters.CommandManager.Models;
using CssFilters.CommandManager.Options;
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
			FilterManager filterManager = new FilterManager(this);
			var groupManager = filterManager.UseGroupManager(Assembly.GetExecutingAssembly());
			filterManager.UseFilterCommandManager()
				.ChangeOptions(o =>
				{
					o.ExecuteFilterMessage = "Фильр {0} выполнил работу с результатом {1}";
					o.WhoCanExecuteMessage = new WhoCanExecuteMessage("Команда доступна только игрокам", "Команда доступна только серверу");
				})
				.AddCommandWithFilters("css_test", "description", new TestCommand1().Handler)
				.AddGroup("TestFilters");
			filterManager.UseFilterAttributeCssHelper();
		}
		#endregion
	}

	public class TestCommand1
	{
		[CommandHelper(minArgs: 2, usage: "О нет команда введена не правильно", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
		public void Handler(CCSPlayerController? player, CommandInfo info)
		{
			Console.WriteLine("Основаня команда выполнена успешно.");
		}
	}

	[GroupName("TestFilters")]
	[FilterName("FilterF1")]
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
	[FilterName("FilterF2")]
	public class Filter2 : FilterCommandBase
	{
		public override void Execute(CCSPlayerController? player, CommandInfo info)
		{
			
		}
	}
}
