using CounterStrikeSharp.API.Core;
using CssFilters.Interfaces;
using CssFilters.Options;

namespace CssFilters
{
	public class FilterManager
	{
		#region Data
		public List<IFilterManager> FilterManagers = new List<IFilterManager>();
		#endregion

		#region Properties
		private OptionsBase optionsBase = new();
		public OptionsBase OptionsBase
		{
			get
			{
				return optionsBase;
			}
		}
		#endregion

		#region Public
		/// <summary>
		/// Включить фильтрацию в плагин.
		/// </summary>
		/// <param name="plugin">Текущий плагин.</param>
		public FilterManager UseFilterManager(BasePlugin plugin)
		{
			OptionsBase.SetPlugin(plugin);
			return this;
		}

		/// <summary>
		/// Изменяет базовые настройки фильтров.
		/// </summary>
		/// <param name="options">Настройки.</param>
		/// <returns></returns>
		public FilterManager ChangeOptions(Action<OptionsBase> options)
		{
			options(OptionsBase);
			return this;
		}
		#endregion
	}
}
