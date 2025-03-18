using CounterStrikeSharp.API.Core;
using CssFilters.Options;

namespace CssFilters.CommandManager.Options;

public class FilterCommandManagerOptions : OptionsBase
{
	#region Properties
	private string executeFilterMessage = "Фильтр {0} завершил работу с реузльтатом {1}";
	public string ExecuteFilterMessage
	{
		get
		{
			return executeFilterMessage;
		}
		set
		{
			executeFilterMessage = value;
		}
	}

	private WhoCanExecuteMessage? whoCanExecuteMessage = null;
	public WhoCanExecuteMessage? WhoCanExecuteMessage
	{
		get
		{
			return whoCanExecuteMessage;
		}
		set
		{
			whoCanExecuteMessage = value;
		}
	}
	#endregion

	#region .ctor
	public FilterCommandManagerOptions(BasePlugin plugin) : base(plugin)
	{}
	#endregion
}

public record WhoCanExecuteMessage(string ServerMessage, string ClientMessage);
