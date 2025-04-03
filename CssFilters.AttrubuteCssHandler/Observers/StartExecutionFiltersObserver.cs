using CounterStrikeSharp.API.Modules.Commands;
using CssFilters.CommandManager.Subjects.Messages;
using CssFilters.Enums.Observer;
using CssFilters.Interface.Observer;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CssFilters.AttrubuteCssHandler.Observers
{
	public class StartExecutionFiltersObserver : IObserverFilterManager<StartExecutionFiltersMessage>
	{
		#region Data
		private FilterAttributeCssHelper _attributeCssHandler;
		#endregion

		#region .ctor
		public StartExecutionFiltersObserver(FilterAttributeCssHelper attributeCssHandler)
		{
			_attributeCssHandler = attributeCssHandler;
		}
		#endregion

		#region Update
		public ObserverContext Update(StartExecutionFiltersMessage message)
		{
			var observerContext = new ObserverContext();
			var commandHelperAttribute = message.CommandHandler.Method.GetCustomAttributes<CommandHelperAttribute>(false);
			if (commandHelperAttribute.Any())
			{
				var minArgs = commandHelperAttribute.Last().MinArgs;
				var usage = commandHelperAttribute.Last().Usage;
				var whoCanExecute = commandHelperAttribute.Last().WhoCanExcecute;
				var filterCommandManager = message.FilterCommandManager;
				var player = message.Player;

				if (filterCommandManager.Options.WhoCanExecuteMessage != null)
				{
					if (whoCanExecute == CommandUsage.CLIENT_ONLY && player == null)
					{
						observerContext.DoIfFailure = () =>
						{
							_attributeCssHandler.Options.Logger?.LogInformation(filterCommandManager.Options.WhoCanExecuteMessage.ServerMessage);
						};
						observerContext.ObserverRuslt = ObserverResult.Failure;
					}

					if (whoCanExecute == CommandUsage.SERVER_ONLY && player != null)
					{
						observerContext.DoIfFailure = () =>
						{
							player.PrintToChat(filterCommandManager.Options.WhoCanExecuteMessage.ClientMessage);
						};
						observerContext.ObserverRuslt = ObserverResult.Failure;
					}

					if (minArgs > message.Info.ArgCount - 1)
					{
						if (player != null)
						{
							player.PrintToChat(usage);
						}
						else
						{
							_attributeCssHandler.Options.Logger?.LogInformation(usage);
						}
						observerContext.ObserverRuslt = ObserverResult.Failure;
					}
				}
			}

			return observerContext;
		}
		#endregion
	}
}
