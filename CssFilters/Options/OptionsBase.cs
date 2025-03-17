using CounterStrikeSharp.API.Core;
using CssFilters.Utilities;
using Microsoft.Extensions.Logging;

namespace CssFilters.Options
{
	/// <summary>
	/// Предоставляет экземпляр <see cref="OptionsBase"./>
	/// </summary>
	public class OptionsBase
	{
		#region Properties
		private BasePlugin? plugin;
		public BasePlugin Plugin
		{
			get
			{
				if(plugin is null)
				{
					throw new ArgumentNullException($"{nameof(OptionsBase)}: В конфигурации не указал плагин.");
				}
				return plugin;
			}
			set
			{
				plugin = value;
			}
		}

		private ILogger? logger;
		public ILogger? Logger
		{
			get
			{
				return logger;
			}
			set
			{
				logger = value;
			}
		}

		private FilterLogger filterLogger;
		public FilterLogger FilterLogger
		{
			get
			{
				return filterLogger ?? throw new ArgumentNullException(nameof(filterLogger));
			}
			set
			{
				filterLogger = value;
			}
		}
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует <see cref="OptionsBase"/>.
		/// </summary>
		/// <param name="plugin">Плагин.</param>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		public OptionsBase(BasePlugin plugin)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		{
			FilterLogger = new FilterLogger(this);
			Plugin = plugin;
			Logger = plugin.Logger;
        }
        #endregion
	}
}
