using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CssFilters.CommandManager.Options;
using CssFilters.CommandManager.Subjects;
using CssFilters.CommandManager.Subjects.Messages;
using CssFilters.Enums;
using CssFilters.Enums.Observer;
using CssFilters.Exceptions.Observer;
using CssFilters.Utilities;
using Microsoft.Extensions.Logging;
using System.Reflection;

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
							observerContextValue.DoIfSuccess();
							break;
						case ObserverResult.Failure:
							observerContextValue.DoIfFailure();
							result = false;
							break;
					}
				}
			}

			return result;
		}

		private bool CheckCsSharpCommandAttributes(CCSPlayerController? player, CommandInfo info)
		{
			var commandHelperAttribute = _mainCommandModel.Handler.Method.GetCustomAttributes<CommandHelperAttribute>(false);
			if (commandHelperAttribute.Any())
			{
				var minArgs = commandHelperAttribute.Last().MinArgs;
				var usage = commandHelperAttribute.Last().Usage;
				var whoCanExecute = commandHelperAttribute.Last().WhoCanExcecute;


				if (_options.WhoCanExecuteMessage != null)
				{
					if (whoCanExecute == CommandUsage.CLIENT_ONLY && player == null)
					{
						_options.Logger?.LogInformation(_options.WhoCanExecuteMessage.ServerMessage);
						return false;
					}

					if (whoCanExecute == CommandUsage.SERVER_ONLY && player != null)
					{
						player.PrintToChat(_options.WhoCanExecuteMessage.ClientMessage);
						return false;
					}
				}

				if (minArgs > info.ArgCount - 1)
				{
					if (player != null)
					{
						player.PrintToChat(usage);
					}
					else
					{
						_options.Logger?.LogInformation(usage);
					}
					return false;
				}
			}
			return true;
		}
		#endregion
	}
}
