namespace CssFilters.CommandManager.Subjects
{
	public class SubjectsRepository
	{
		/// <summary>
		/// Оповещает перед выполнением фильтров.
		/// </summary>
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
