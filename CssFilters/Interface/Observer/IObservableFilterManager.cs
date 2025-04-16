namespace CssFilters.Interface.Observer
{
	/// <summary>
	/// Предсталяет интерфейс наблюдаемего объекта.
	/// </summary>
	/// <typeparam name="T">Сообщение.</typeparam>
	public interface IObservableFilterManager<T>
	{
		/// <summary>
		/// Добавляет наблюдателя.
		/// </summary>
		/// <param name="observer"></param>
		void AddObserver(IObserverFilterManager<T> observer);

		/// <summary>
		/// Удаляет наблюдателя.
		/// </summary>
		/// <param name="observer"></param>
		void RemoveObserver(IObserverFilterManager<T> observer);

		/// <summary>
		/// Уведомляет наблюдателей.
		/// </summary>
		/// <param name="message">Сообщение.</param>
		void NotifyObservers(T message);
	}
}
