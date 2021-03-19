using System.Collections.Generic;
using ConcurrencyHandlingSample.Model;

namespace ConcurrencyHandlingSample.Infrastructure
{
	public class DomainEventsHelper
	{
		public static IReadOnlyCollection<IDomainEvent> GetAllDomainEvents(AggregateRoot aggregate)
		{
			return aggregate.DomainEvents;
		}
	}
}