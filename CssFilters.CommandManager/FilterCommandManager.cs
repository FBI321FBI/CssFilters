using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CssFilters.CommandManager.Models;
using CssFilters.CommandManager.Options;
using CssFilters.Models;

namespace CssFilters.CommandManager
{
	public class FilterCommandManager : FilterManagerBase<FilterCommandManagerOptions>
	{
		#region Data
		private BasePlugin _plugin => Options.Plugin;

		private HashSet<CommandWithFilters> _commandsWithFilters = new HashSet<CommandWithFilters>();
		#endregion

		#region .ctor
		public FilterCommandManager(FilterManager filterManager)
			: base(filterManager)
		{
		}
		#endregion

		#region Public
		/// <summary>
		/// Создать команду с фильтрами.
		/// </summary>
		/// <param name="name">Название команд.</param>
		/// <param name="description">Описание команды.</param>
		/// <param name="handler">Обработчик команды.</param>
		/// <returns></returns>
		public CommandWithFilters AddCommandWithFilters(string name, string description, CommandInfo.CommandCallback handler)
		{
			_plugin.AddCommand(name, description, handler);
			var commandWithFilters = new CommandWithFilters(name, description, handler, this);
			_commandsWithFilters.Add(commandWithFilters);
			return commandWithFilters;
		}

		/// <summary>
		/// Изменяет настройки фильтра.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public override FilterCommandManager ChangeOptions(Action<FilterCommandManagerOptions> options)
		{
			options(Options);
			return this;
		}
		#endregion
	}
}
