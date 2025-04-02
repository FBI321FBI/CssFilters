using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;

namespace CssFilters.CommandManager.Subjects.Messages
{
    public record StartExecutionFiltersMessage(
        string CommandName, 
        string CommandDescription,
        CommandInfo.CommandCallback CommandHandler,
        CCSPlayerController? Player,
        CommandInfo Info,
        FilterCommandManager FilterCommandManager);
}
