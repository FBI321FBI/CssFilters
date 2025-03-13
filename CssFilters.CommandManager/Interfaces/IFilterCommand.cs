using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;

namespace CssFilters.CommandManager.Interfaces
{
	public interface IFilterCommand
	{
		/// <summary>
		/// Выполнение фильтра.
		/// </summary>
		/// <param name="player">Игрок.</param>
		/// <param name="info">Инфо.</param>
		/// <returns></returns>
		public void Execute(CCSPlayerController? player, CommandInfo info);
	}
}
