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
			private set
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
        public OptionsBase()
        {
			filterLogger = new FilterLogger();
        }
        #endregion

        #region Public
        public void SetPlugin(BasePlugin plugin)
		{
			Plugin = plugin;
			Logger = plugin.Logger;
			FilterLogger.Prepare(this);
		}
		#endregion
	}
}
