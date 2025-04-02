using CssFilters.Models.Observer;

namespace CssFilters.Interface.Observer
{
	public interface IObserverFilterManager<T>
	{
		ObserverContext Update(T message);
	}
}
