using CssFilters.CommandManager.Models;

namespace CssFilters.GroupManager.Extensions
{
	public static class CommandWithFiltersExtension
	{
		public static CommandWithFilters AddGroup(this CommandWithFilters commandWithFilters, string groupName)
		{
			var groupManager = commandWithFilters.FilterCommandManager.FilterManager.FilterManagers.Where(x => x is GroupManager).SingleOrDefault() as GroupManager;
			if (groupManager == null)
			{
				throw new InvalidOperationException($"{nameof(GroupManager)} не найден в {nameof(FilterManager)}. Возможно пропущен метод Use.");
			}

			var groupCommandModel = groupManager.GroupCommandsModels.Where(x => x.Name == groupName).SingleOrDefault()
				?? throw new ArgumentNullException($"Группа с названием {groupName} не найдена.");

			foreach (var commandFilter in groupCommandModel.CommandFilters)
			{
				commandWithFilters.AddFilter(commandFilter);
			}

			return commandWithFilters;
		}
	}
}
