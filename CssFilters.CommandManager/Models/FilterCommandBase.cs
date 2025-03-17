using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CssFilters.Attributes;
using CssFilters.CommandManager.Interfaces;
using CssFilters.Enums;
using CssFilters.Filters;

namespace CssFilters.CommandManager.Models
{
	/// <summary>
	/// Базовый класс фильтра.
	/// </summary>
	/// <typeparam name="T">Фильтр.</typeparam>
	public abstract class FilterCommandBase : IFilterCommand
	{
		#region Properties
		private FilterContext filterContext;
		/// <summary>
		/// Контекст фильтра.
		/// </summary>
		internal FilterContext FilterContext
		{
			get
			{
				if (filterContext is not null)
				{
					var oldFilterContext = filterContext;
					FilterContext = new FilterContext();
					return oldFilterContext;
				}
				else
				{
					return filterContext!;
				}
			}
			private set
			{
				filterContext = value;
			}
		}
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует <see cref="FilterCommandBase"/>.
		/// </summary>
		protected FilterCommandBase()
		{
			filterContext = new FilterContext();
		}
		#endregion

		#region Public
		///<inheritdoc/>
		public abstract void Execute(CCSPlayerController? player, CommandInfo info);

		/// <summary>
		/// Получение контекста фильтра.
		/// </summary>
		/// <returns></returns>
		public FilterContext GetFilterContext()
		{
			return filterContext;
		}

		/// <summary>
		/// Устанавливает фильтру результат выполнения.
		/// </summary>
		/// <param name="filterResult"></param>
		public void SetFilterResult(FilterResults filterResult)
		{
			filterContext.FilterReult = filterResult;
		}
		#endregion

		#region Internal
		/// <summary>
		/// Возвращает название атрибута.
		/// </summary>
		/// <returns></returns>
		internal string? GetFilterName()
		{
			var attribute = GetType().GetCustomAttributes(typeof(FilterNameAttribute), false);
			if (attribute.Any())
			{
				var filterNameAttribute = attribute.Last() as FilterNameAttribute;
				return filterNameAttribute?.Name;
			}
			return null;
		}
		#endregion
	}
}
