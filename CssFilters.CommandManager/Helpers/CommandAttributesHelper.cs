using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CssFilters.CommandManager.Helpers
{
	/// <summary>
	/// Помощник по атрибутам, которые предназначены командам.
	/// </summary>
	public static class CommandAttributesHelper
	{
		/// <summary>
		/// Проверяет команду на валидность <see cref="CommandHelperAttribute"/>.
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <param name="info"><see cref="CommandInfo"/>.</param>
		public static bool CommandValidate(
			CCSPlayerController? player,
			CommandInfo info,
			CommandInfo.CommandCallback handler,
			string? serverMessage,
			string? clientMessage,
			ILogger? logger)
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
						logger?.LogInformation(serverMessage);
						return false;
					}

					if (whoCanExecute == CommandUsage.SERVER_ONLY && player != null)
					{
						player.PrintToChat(clientMessage);
						return false;
					}

					if (minArgs > info.ArgCount - 1)
					{
						if (player != null)
						{
							player.PrintToChat(usage);
						}
						else
						{
							logger?.LogInformation(usage);
						}
						return false;
					}
				}
			}
			return true;
		}
	}
}
