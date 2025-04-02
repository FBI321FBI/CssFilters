using CssFilters.CommandManager.Subjects.Messages;
using CssFilters.Interface.Observer;
using CssFilters.Models.Observer;

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

		public Dictionary<string, ObserverContext> Notify(StartExecutionFiltersMessage message)
		{
			var observerContexts = new Dictionary<string, ObserverContext>();
			foreach (var observer in _observers)
			{
				var context = observer.Update(message);
				observerContexts.Add(observer.GetType().Name, context);
			}
			return observerContexts;
		}
	}
}
