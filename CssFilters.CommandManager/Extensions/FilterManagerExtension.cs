namespace CssFilters.CommandManager.Extensions
{
	public static class FilterManagerExtension
	{
		/// <summary>
		/// Добавляет возможность создавать команды с фильтрами.
		/// </summary>
		/// <param name="filterManager"></param>
		/// <returns></returns>
		public static FilterCommandManager UseFilterCommandManager(this FilterManager filterManager)
		{
			var commandManager = new FilterCommandManager(filterManager);
			return commandManager;
		}
	}
}
