using CounterStrikeSharp.API.Modules.Commands;

namespace CssFilters.CommandManager.Models
{
	internal class MainCommandModel
	{
		#region Data
		public string Name;
		public string Description;
		public CommandInfo.CommandCallback Handler;
		#endregion

		#region .ctor
		public MainCommandModel(string name, string description, CommandInfo.CommandCallback handler)
		{
			Name = name;
			Description = description;
			Handler = handler;
		}
		#endregion
	}
}
