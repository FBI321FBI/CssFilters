using CssFilters.CommandManager.Models;

namespace CssFilters.GroupManager.Models
{
	public class GroupCommandsModel
	{
		#region Data
		public string? Name;
		public List<FilterCommandBase> CommandFilters;
		#endregion

		#region .ctor
		public GroupCommandsModel(string name, List<FilterCommandBase> commandFilters)
		{
			Name = name;
			CommandFilters = commandFilters;
		}
		#endregion
	}
}
