namespace CssFilters.Interface.Observer
{
	public interface IObserverFilterManager<T>
	{
		void Update(T message);
	}
}
