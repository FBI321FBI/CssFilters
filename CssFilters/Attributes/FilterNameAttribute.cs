namespace CssFilters.Attributes
{
	/// <summary>
	/// Название фильтра.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	public class FilterNameAttribute : Attribute
	{
		public string Name;

		public FilterNameAttribute(string name)
		{
			Name = name;
		}
	}
}
