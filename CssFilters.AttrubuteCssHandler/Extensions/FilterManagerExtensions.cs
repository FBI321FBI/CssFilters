namespace CssFilters.AttrubuteCssHandler.Extensions
{
	public static class FilterManagerExtensions
	{
		public static FilterAttributeCssHelper UseFilterAttributeCssHelper(this FilterManager filterManager)
		{
			var filterAttributeCssHelper = new FilterAttributeCssHelper(filterManager);
			return filterAttributeCssHelper;
		}
	}
}
