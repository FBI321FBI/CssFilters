using CounterStrikeSharp.API.Core;
using CssFilters.CommandManager.Models;
using CssFilters.GroupManager.Models;
using CssFilters.GroupManager.Options;
using CssFilters.Interfaces;

namespace CssFilters.GroupManager
{
	public class GroupManager : IFilterManager
	{
		#region Data
		private BasePlugin _plugin => Options.Plugin;
		private FilterManager _filterManager;

		internal HashSet<GroupCommandsModel> GroupCommandsModels;
		#endregion

		#region Properties
		private GroupManagerOptions options = new();
		public GroupManagerOptions Options
		{
			get
			{
				return options;
			}
		}
		#endregion

		#region .ctor
		public GroupManager(FilterManager filterManager)
		{
			GroupCommandsModels = new HashSet<GroupCommandsModel>();
			_filterManager = filterManager;
		}
		#endregion

		#region Public
		/// <summary>
		/// Добавление группы для фильтров.
		/// </summary>
		/// <param name="name">Название группы.</param>
		/// <param name="filterCommands">Фильтр.</param>
		/// <returns></returns>
		public GroupManager AddGroupForCommands(string name, List<FilterCommandBase> filterCommands)
		{
			GroupCommandsModels.Add(new GroupCommandsModel(name, filterCommands));
			return this;
		}

		/// <summary>
		/// Изменяет настройки менеджера групп.
		/// </summary>
		/// <param name="options">Настройки.</param>
		/// <returns></returns>
		public GroupManager ChangeOptions(Action<GroupManagerOptions> options)
		{
			options(Options);
			return this;
		}
		#endregion

		#region Private
		private void SetGroupNameAttributes()
		{
			
		}
		#endregion
	}
}
