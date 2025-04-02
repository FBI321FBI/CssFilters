using CssFilters.Models.Observer;

namespace CssFilters.Interface.Observer
{
	public interface ISubjectFilterManager<T>
	{
		/// <summary>
		/// Подписаться.
		/// </summary>
		/// <param name="observer"></param>
		void Attach(IObserverFilterManager<T> observer);

		/// <summary>
		/// Отписаться.
		/// </summary>
		/// <param name="observer"></param>
		void Detach(IObserverFilterManager<T> observer);

		/// <summary>
		/// Оповестить.
		/// </summary>
		/// <param name="message">Сообщение.</param>
		Dictionary<string, ObserverContext> Notify(T message);
	}
}
