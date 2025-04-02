namespace CssFilters.CommandManager.Subjects
{
	public class SubjectsRepository
	{
		public StartExecutionFiltersSubject StartExecutionFiltersSubject 
		{ 
			get; 
			init;
		}

        #region .ctor
        public SubjectsRepository(
			StartExecutionFiltersSubject startExecutionFiltersSubject)
        {
			StartExecutionFiltersSubject = startExecutionFiltersSubject;
		}
        #endregion
    }
}
