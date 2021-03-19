using System.Collections.Generic;

namespace ConcurrencyHandlingSample.Model
{
	public class AggregateRoot
	{
		private int _versionId;

		public void IncreaseVersion()
		{
			_versionId++;
		}

		private List<IDomainEvent> _domainEvents;

		public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

		protected void AddDomainEvent(IDomainEvent domainEvent)
		{
			_domainEvents ??= new List<IDomainEvent>();

			_domainEvents.Add(domainEvent);
		}
	}
}