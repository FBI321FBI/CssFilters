namespace CssFilters.GroupManager.Attributes
{
	/// <summary>
	/// Название группы.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class GroupNameAttribute : Attribute
	{
		#region Data
		public string GroupName;
		#endregion

		#region .ctor
		public GroupNameAttribute(string groupName)
		{
			GroupName = groupName;
		}
		#endregion
	}
}
