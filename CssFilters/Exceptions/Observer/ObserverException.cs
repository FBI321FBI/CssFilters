namespace CssFilters.Exceptions.Observer
{
	/// <summary>
	/// Исключение созданное внутри наблюдателя.
	/// </summary>
	public class ObserverException : Exception
	{
		/// <summary>
		/// Инициализирует экземпляр <see cref="ObserverException"/>.
		/// </summary>
		/// <param name="message">Сообщение об ошибке.</param>
		public ObserverException(string message)
			: base(message) { }
	}
}
