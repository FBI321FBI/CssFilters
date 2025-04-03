using CssFilters.CommandManager.Subjects.Messages;
using CssFilters.Interface.Observer;

namespace CssFilters.CommandManager.Subjects
{
	/// <summary>
	/// Оповещает перед выполнением фильтров.
	/// </summary>
	public class StartExecutionFiltersSubject : ISubjectFilterManager<StartExecutionFiltersMessage>
	{
		private List<IObserverFilterManager<StartExecutionFiltersMessage>> _observers = [];

		public void Attach(IObserverFilterManager<StartExecutionFiltersMessage> observer)
		{
			_observers.Add(observer);
		}

		public void Detach(IObserverFilterManager<StartExecutionFiltersMessage> observer)
		{
			_observers.Remove(observer);
		}

		public void Notify(StartExecutionFiltersMessage message)
		{
			foreach (var observer in _observers)
			{
				observer.Update(message);
			}
		}
	}
}
