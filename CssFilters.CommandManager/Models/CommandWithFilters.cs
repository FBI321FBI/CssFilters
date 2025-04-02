using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CssFilters.CommandManager.Options;
using CssFilters.CommandManager.Subjects;
using CssFilters.CommandManager.Subjects.Messages;
using CssFilters.Enums;
using CssFilters.Enums.Observer;
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
		private FilterCommandManagerOptions _options => FilterCommandManager.Options;

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
			if (!StartExecutionFiltersSubject(_mainCommandModel, player, info)) return;

			foreach (var filter in _filterCommands)
			{
				filter.Execute(player, info);
				var filterContext = filter.FilterContext;
				switch (filterContext.FilterReult)
				{
					case FilterResults.Next:
						_filterLogger.LogInformation(string.Format(_options.ExecuteFilterMessage, filter.GetFilterName() ?? filter.GetType().Name, FilterResults.Next));
						continue;
					case FilterResults.Stop:
						_filterLogger.LogInformation(string.Format(_options.ExecuteFilterMessage, filter.GetFilterName() ?? filter.GetType().Name, FilterResults.Stop));
						return;
					case FilterResults.Step:
						_mainCommandModel.Handler.Invoke(player, info);
						return;
				}
			}
			_mainCommandModel.Handler.Invoke(player, info);
			_filterLogger.LogInformation($"Команда {_mainCommandModel.Name} была выполнена.");
		}

		private bool StartExecutionFiltersSubject(MainCommandModel mainCommand, CCSPlayerController? player, CommandInfo info)
		{
			var result = true;
			var subject = FilterCommandManager.GetSubject<StartExecutionFiltersSubject>();
			var observerContexts = subject?.Notify(
				new StartExecutionFiltersMessage(
					mainCommand.Name,
					mainCommand.Description,
					mainCommand.Handler,
					player,
					info,
					FilterCommandManager));
			if (observerContexts != null)
			{
				foreach (var observerContext in observerContexts)
				{
					var observerContextValue = observerContext.Value;
					switch (observerContextValue.ObserverRuslt)
					{
						case ObserverResult.Success:
							observerContextValue.DoIfSuccess.Invoke();
							break;
						case ObserverResult.Failure:
							observerContextValue.DoIfFailure.Invoke();
							result = false;
							break;
					}

				}
			}

			return result;
		}
		#endregion
	}
}
