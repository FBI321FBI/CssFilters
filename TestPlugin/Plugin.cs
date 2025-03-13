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
			_filterManager = new FilterManager();
		}
		#endregion

		#region Override
		public override void Load(bool hotReload)
		{
			_filterManager.UseFilterManager(this)
				.UseGroupManager()
				.AddGroupForCommands("TestGroup", new List<FilterCommandBase>
				{
					new TestCommandFilter(), new TestCommandFilter2()
				});

			_filterManager.UseFilterCommandManager()
				.AddCommandWithFilters("css_test", "Тестовая команда.", (p, i) =>
				{
					Console.WriteLine("Всем привет.");
				});
		}
		#endregion
	}

	[GroupName("TestGroup")]
	[FilterName("Filter1")]
	public class TestCommandFilter : FilterCommandBase
	{
		public override void Execute(CCSPlayerController? player, CommandInfo info)
		{
			Console.WriteLine("Фильтр1 запущен");
			SetFilterResult(FilterResults.Next);
		}
	}

	[GroupName("TestGroup")]
	[FilterName("Filter2")]
	public class TestCommandFilter2 : FilterCommandBase
	{
		public override void Execute(CCSPlayerController? player, CommandInfo info)
		{
			Console.WriteLine("Фильтр2 запущен.");
			SetFilterResult(FilterResults.Next);
		}
	}
}
