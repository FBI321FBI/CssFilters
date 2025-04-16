namespace CssFilters.Interface.Observer
{
	/// <summary>
	/// Представляет интерфейс наблюдателя.
	/// </summary>
	/// <typeparam name="T">Отправляемое сообщение.</typeparam>
	public interface IObserverFilterManager<T>
	{
		/// <summary>
		/// Выполняется при оповещении наблюдателя.
		/// </summary>
		/// <param name="message">Сообщение.</param>
		void Update(T message);
	}
}
