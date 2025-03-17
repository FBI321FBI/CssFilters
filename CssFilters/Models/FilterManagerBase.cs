using CounterStrikeSharp.API.Core;
using CssFilters.Interface;
using CssFilters.Options;
using CssFilters.Utilities;

namespace CssFilters.Models
{
	/// <summary>
	/// Базовый класс фильтров.
	/// </summary>
	public abstract class FilterManagerBase<TOptions> : IFilterManager where TOptions : OptionsBase
	{
		#region Properties
		private TOptions options;
		public TOptions Options
		{
			get
			{
				return options;
			}
		}
		#endregion

		#region Data
		/// <summary>
		/// Менеджер фильтров.
		/// </summary>
		public FilterManager FilterManager;

		/// <summary>
		/// Логгер фильтра.
		/// </summary>

		public FilterLogger FilterLogger => Options.FilterLogger;

		/// <summary>
		/// Плагин.
		/// </summary>
		public BasePlugin Plugin => Options.Plugin;
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует экземпляр <see cref="FilterManagerBase"/>.
		/// </summary>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		protected FilterManagerBase(FilterManager filterManager)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		{
			FilterManager = filterManager;
			options = (TOptions)Activator.CreateInstance(typeof(TOptions), filterManager.OptionsBase.Plugin)!;
			Prepare();
		}
		#endregion

		#region Public
		/// <summary>
		/// Изменяет настройки фильтра.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public virtual IFilterManager ChangeOptions(Action<TOptions> options)
		{
			options(Options);
			return this;
		}
		#endregion

		#region Private
		private void Prepare()
		{
			FilterManager.FilterManagers.Add(this);
			ChangeOptions(o =>
			{
				o.FilterLogger = FilterManager.OptionsBase.FilterLogger;
				o.Logger = FilterManager.OptionsBase.Logger;
				o.Plugin = FilterManager.OptionsBase.Plugin;
			});
		}
		#endregion
	}
}
