using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CssFilters.Enums;
using CssFilters.Utilities;

namespace CssFilters.CommandManager.Models
{
	public class CommandWithFilters
	{
		#region Data
		private MainCommandModel _mainCommandModel;
		private List<FilterCommandBase> _filterCommands;
		private BasePlugin _plugin => FilterCommandManager.Options.Plugin;
		private FilterLogger _filterLogger => FilterCommandManager.Options.FilterLogger;

		public FilterCommandManager FilterCommandManager;
		#endregion

		#region .ctor
		/// <summary>
		/// Реализует экземпляр <see cref="CommandWithFilters"/>.
		/// </summary>
		/// <param name="name">Название команды.</param>
		/// <param name="description">Описание команды.</param>
		/// <param name="handler">Обработчик команды.</param>
		public CommandWithFilters(string name, string description, CommandInfo.CommandCallback handler, FilterCommandManager filterCommandManager)
		{
			_mainCommandModel = new MainCommandModel(name, description, handler);
			_filterCommands = new List<FilterCommandBase>();
			FilterCommandManager = filterCommandManager;
		}
		#endregion

		#region Public
		/// <summary>
		/// Добавить фильтр.
		/// </summary>
		/// <param name="filterCommand">Фильтр.</param>
		/// <returns></returns>
		public CommandWithFilters AddFilter(FilterCommandBase filterCommand)
		{
			_filterCommands.Add(filterCommand);
			_plugin.RemoveCommand(_mainCommandModel.Name, _mainCommandModel.Handler);
			_plugin.RemoveCommand(_mainCommandModel.Name, Handler);
			_plugin.AddCommand(_mainCommandModel.Name, _mainCommandModel.Description, Handler);
			return this;
		}
		#endregion

		#region Private
		private void Handler(CCSPlayerController? player, CommandInfo info)
		{
			foreach (var filter in _filterCommands)
			{
				filter.Execute(player, info);
				var filterContext = filter.FilterContext;
				switch (filterContext.FilterReult)
				{
					case FilterResults.Next:
						_filterLogger.LogInforamtion($"Фильтр {filter.GetFilterName() ?? filter.GetType().Name} завершил работу с реузльтатом {FilterResults.Next}");
						continue;
					case FilterResults.Stop:
						_filterLogger.LogInforamtion($"Фильтр {filter.GetFilterName() ?? filter.GetType().Name} завершил работу с реузльтатом {FilterResults.Stop}");
						return;
				}
			}
			_mainCommandModel.Handler.Invoke(player, info);
			_filterLogger.LogInforamtion($"Команда {_mainCommandModel.Name} была выполнена.");
		}
		#endregion
	}
}
