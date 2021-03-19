using ConcurrencyHandlingSample.Model;

namespace ConcurrencyHandlingSample.Application
{
	public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
	{
		void Handle(TEvent @event);
	}
}