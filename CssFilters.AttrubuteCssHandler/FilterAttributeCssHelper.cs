using CssFilters.AttrubuteCssHandler.Observers;
using CssFilters.AttrubuteCssHandler.Options;
using CssFilters.CommandManager;
using CssFilters.CommandManager.Subjects;
using CssFilters.Models;

namespace CssFilters.AttrubuteCssHandler
{
	public class FilterAttributeCssHelper : FilterManagerBase<AttributeCssHandlerOptions>
	{
		#region .ctor
		public FilterAttributeCssHelper(FilterManager filterManager) 
			: base(filterManager)
		{
			RegisterObservers();
		}
		#endregion

		#region Private
		private void RegisterObservers()
		{
			var filterManager = FilterManager.FilterManagers.Where(x => x.GetType().Name == typeof(FilterCommandManager).Name).SingleOrDefault();
			if (filterManager != null) 
			{
				var filterCommandManager = (FilterCommandManager)filterManager;
				var startExecutionFiltersSubject = filterCommandManager.GetSubject<StartExecutionFiltersSubject>();

				startExecutionFiltersSubject?.Attach(new StartExecutionFiltersObserver(this));
			}
		}
		#endregion
	}
}
