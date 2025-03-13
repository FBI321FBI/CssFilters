namespace CssFilters.CommandManager.Extensions
{
	public static class FilterManagerExtension
	{
		/// <summary>
		/// Добавляет возможность создавать команды с фильтрами.
		/// </summary>
		/// <param name="filterCommandManager"></param>
		/// <returns></returns>
		public static FilterCommandManager UseFilterCommandManager(this FilterManager filterCommandManager)
		{
			var commandManager = new FilterCommandManager(filterCommandManager);
			filterCommandManager.FilterManagers.Add(commandManager);
			commandManager.ChangeOptions(o =>
			{
				o.SetPlugin(filterCommandManager.OptionsBase.Plugin);
				o.FilterLogger = filterCommandManager.OptionsBase.FilterLogger;
			});
			return commandManager;
		}
	}
}
