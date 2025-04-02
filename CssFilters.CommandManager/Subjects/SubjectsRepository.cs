namespace CssFilters.CommandManager.Subjects
{
	internal class SubjectsRepository
	{
		internal StartExecutionFiltersSubject StartExecutionFiltersSubject 
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
