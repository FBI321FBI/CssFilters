using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CssFilters.CommandManager.Models;
using CssFilters.CommandManager.Options;
using CssFilters.CommandManager.Subjects;
using CssFilters.Models;

namespace CssFilters.CommandManager
{
	public class FilterCommandManager : FilterManagerBase<FilterCommandManagerOptions>
	{
		#region Data
		private BasePlugin _plugin => Options.Plugin;

		private HashSet<CommandWithFilters> _commandsWithFilters = new HashSet<CommandWithFilters>();

		private SubjectsRepository _subjectsRepository;
		#endregion

		#region .ctor
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		public FilterCommandManager(FilterManager filterManager)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
			: base(filterManager)
		{
			AddSubjectsRepository();
		}
		#endregion

		#region Public
		/// <summary>
		/// Создать команду с фильтрами.
		/// </summary>
		/// <param name="name">Название команд.</param>
		/// <param name="description">Описание команды.</param>
		/// <param name="handler">Обработчик команды.</param>
		/// <returns></returns>
		public CommandWithFilters AddCommandWithFilters(string name, string description, CommandInfo.CommandCallback handler)
		{
			_plugin.AddCommand(name, description, handler);
			var commandWithFilters = new CommandWithFilters(name, description, handler, this);
			_commandsWithFilters.Add(commandWithFilters);
			return commandWithFilters;
		}

		/// <summary>
		/// Изменяет настройки фильтра.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public override FilterCommandManager ChangeOptions(Action<FilterCommandManagerOptions> options)
		{
			options(Options);
			return this;
		}

		/// <summary>
		/// Получение субъекта.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public T? GetSubject<T>()
		{
			var propertyName = typeof(T).Name;
			var propertyInfo = _subjectsRepository.GetType().GetProperty(propertyName);
			if (propertyInfo == null)
			{
				throw new InvalidOperationException($"Субъект {propertyName} не найден.");
			}

			var property = propertyInfo.GetValue(_subjectsRepository);

			return property != null ? (T)property : default(T);
		}
		#endregion

		#region Private
		private void AddSubjectsRepository()
		{
			_subjectsRepository = new SubjectsRepository(
				new StartExecutionFiltersSubject());
		}
		#endregion
	}
}
