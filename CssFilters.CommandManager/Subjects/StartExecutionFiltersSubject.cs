using CssFilters.CommandManager.Subjects.Messages;
using CssFilters.Interface.Observer;

namespace CssFilters.CommandManager.Subjects
{
	/// <summary>
	/// Оповещает перед выполнением фильтров.
	/// </summary>
	public class StartExecutionFiltersSubject : IObservableFilterManager<StartExecutionFiltersMessage>
	{
		private List<IObserverFilterManager<StartExecutionFiltersMessage>> _observers = [];

		public void AddObserver(IObserverFilterManager<StartExecutionFiltersMessage> observer)
		{
			_observers.Add(observer);
		}

		public void RemoveObserver(IObserverFilterManager<StartExecutionFiltersMessage> observer)
		{
			_observers.Remove(observer);
		}

		public void NotifyObservers(StartExecutionFiltersMessage message)
		{
			foreach (var observer in _observers)
			{
				observer.Update(message);
			}
		}
	}
}
