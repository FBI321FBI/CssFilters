using CssFilters.Options;
using Microsoft.Extensions.Logging;

namespace CssFilters.Utilities
{
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
		private ILogger? _logger => FilterOptionsBase.Logger;
		#endregion

		#region Public
		/// <summary>
		/// Настраивает логгер.
		/// </summary>
		/// <param name="filterOptionsBase"></param>
		public void Prepare(OptionsBase filterOptionsBase)
		{
			FilterOptionsBase = filterOptionsBase;
		}

		/// <summary>
		/// Вывести информацию в консоль.
		/// </summary>
		public void LogInforamtion(string message)
		{
			if (_logger is null) return;
			_logger.LogInformation(message);
		}

		/// <summary>
		/// Вывести придупреждение в консоль.
		/// </summary>
		/// <param name="message"></param>
		public void LogWarning(string message)
		{
			if (_logger is null) return;
			_logger.LogWarning(message);
		}

		/// <summary>
		/// Вывести ошибку в консоль.
		/// </summary>
		/// <param name="message"></param>
		public void LogError(string message)
		{
			if (_logger is null) return;
			_logger.LogError(message);
		}
		#endregion
	}
}
