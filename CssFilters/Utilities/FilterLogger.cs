using CssFilters.Options;
using Microsoft.Extensions.Logging;

namespace CssFilters.Utilities
{
	/// <summary>
	/// Логгер фильтров.
	/// </summary>
	public class FilterLogger
	{
		#region Properties
		private OptionsBase? filterOptionsBase;
		public OptionsBase FilterOptionsBase
		{
			get
			{
				if (filterOptionsBase is null)
				{
					throw new ArgumentNullException(nameof(filterOptionsBase));
				}
				return filterOptionsBase;
			}
			private set
			{
				filterOptionsBase = value;
			}
		}
		#endregion

		#region Data
		public ILogger? Logger => FilterOptionsBase.Logger;
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует <see cref="FilterLogger"/>.
		/// </summary>
		public FilterLogger(OptionsBase filterOptionsBase)
		{
			FilterOptionsBase = filterOptionsBase;
		}
		#endregion

		#region Public
		/// <summary>
		/// Вывести информацию в консоль.
		/// </summary>
		public void LogInformation(string message)
		{
			if (Logger is null) return;
			Logger.LogInformation(message);
		}

		/// <summary>
		/// Вывести придупреждение в консоль.
		/// </summary>
		/// <param name="message"></param>
		public void LogWarning(string message)
		{
			if (Logger is null) return;
			Logger.LogWarning(message);
		}

		/// <summary>
		/// Вывести ошибку в консоль.
		/// </summary>
		/// <param name="message"></param>
		public void LogError(string message)
		{
			if (Logger is null) return;
			Logger.LogError(message);
		}
		#endregion
	}
}
