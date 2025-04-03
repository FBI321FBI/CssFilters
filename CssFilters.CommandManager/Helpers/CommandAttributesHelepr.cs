using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CssFilters.CommandManager.Options;
using CssFilters.Enums.Observer;
using System.Reflection;

namespace CssFilters.CommandManager.Helpers
{
	/// <summary>
	/// Помощник по атрибутам, которые предназначены командам.
	/// </summary>
	public static class CommandAttributesHelepr
	{
		/// <summary>
		/// Проверяет команду на валидность <see cref="CommandHelperAttribute"/>.
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <param name="info"><see cref="CommandInfo"/>.</param>
		public static void CommandValidate(
			CCSPlayerController? player, 
			CommandInfo info, 
			CommandInfo.CommandCallback handler,
			string serverMessage,
			string clientMessage)
		{
			var commandHelperAttribute = handler.Method.GetCustomAttributes<CommandHelperAttribute>(false);
			if (commandHelperAttribute.Any())
			{
				var minArgs = commandHelperAttribute.Last().MinArgs;
				var usage = commandHelperAttribute.Last().Usage;
				var whoCanExecute = commandHelperAttribute.Last().WhoCanExcecute;

				if (!string.IsNullOrEmpty(serverMessage) && !string.IsNullOrEmpty(clientMessage))
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
		}
	}
}
