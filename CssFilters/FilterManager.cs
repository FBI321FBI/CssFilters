using CounterStrikeSharp.API.Core;
using CssFilters.Interface;
using CssFilters.Models;
using CssFilters.Options;

namespace CssFilters
{
	/// <summary>
	/// Предоставляет менеджера по фильтрам.
	/// </summary>
	public class FilterManager
	{
		#region Data
		public List<IFilterManager> FilterManagers = new List<IFilterManager>();
		#endregion

		#region Properties
		private OptionsBase optionsBase;
		public OptionsBase OptionsBase
		{
			get
			{
				return optionsBase;
			}
		}
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует экземпляр <see cref="FilterManager"/>
		/// </summary>
		public FilterManager(BasePlugin plugin)
		{
			optionsBase = new(plugin);
		}
		#endregion

		#region Public
		/// <summary>
		/// Возвращает настройки фильтра.
		/// </summary>
		/// <typeparam name="T">Класс настроек.</typeparam>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public T GetOptions<T>()
		{
			foreach (var filterManager in FilterManagers)
			{
				var filterManagerBase = filterManager as FilterManagerBase<OptionsBase>;
				if(filterManagerBase?.Options is T filterManagerBaseOptions)
				{
					return filterManagerBaseOptions;
				}
			}
			throw new InvalidOperationException($"Настройки {nameof(T)} не были найдены. Возможно фильтр не зарегистрирован, либо не наследуется от FilterManagerBase.");
		}

		/// <summary>
		/// Возвращает указанного менеджера фильтров.
		/// </summary>
		/// <typeparam name="T">Менеджер.</typeparam>
		/// <returns></returns>
		public T? GetManager<T>()
		{
			foreach (var filterManager in FilterManagers)
			{
				if(filterManager is T)
				{
					return (T)filterManager;
				}
			}
			return default(T);
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
