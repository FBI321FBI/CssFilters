using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CssFilters.CommandManager.Models;
using CssFilters.CommandManager.Options;
using CssFilters.Interfaces;

namespace CssFilters.CommandManager
{
	public class FilterCommandManager : IFilterManager
	{
		#region Data
		private BasePlugin _plugin => Options.Plugin;

		private HashSet<CommandWithFilters> _commandsWithFilters = new HashSet<CommandWithFilters>();
		public FilterManager FilterManager;
		#endregion

		#region Properties
		private FilterCommandManagerOptions options = new();
		public FilterCommandManagerOptions Options
		{
			get
			{
				return options;
			}
		}
		#endregion

		#region .ctor
		public FilterCommandManager(FilterManager filterManager)
		{
			FilterManager = filterManager;
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
		/// Изменяет настройки фильтров комманд.
		/// </summary>
		/// <param name="options">Настройки.</param>
		/// <returns></returns>
		public FilterCommandManager ChangeOptions(Action<FilterCommandManagerOptions> options)
		{
			options(Options);
			return this;
		}
		#endregion
	}
}
