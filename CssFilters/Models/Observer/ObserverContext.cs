using CssFilters.Enums.Observer;
using CssFilters.Exceptions.Observer;

namespace CssFilters.Models.Observer
{
	public class ObserverContext
	{
		/// <summary>
		/// Результат оповещения.
		/// </summary>
		public ObserverResult ObserverRuslt
		{
			get;
			set;
		}

		/// <summary>
		/// Выполнить если результат <see cref="ObserverResult.Success"/>.
		/// </summary>
		public Action DoIfSuccess = () => { };

		/// <summary>
		/// Выполнить если результат <see cref="ObserverResult.Failure"/>.
		/// </summary>
		public Action DoIfFailure = () => { };
	}
}
