using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConcurrencyHandlingSample.Model;

namespace ConcurrencyHandlingSample.Application
{
	public class EventDispatcher // Interface segregation
	{
		public void Dispatch<TEvent>(IReadOnlyCollection<TEvent> events) where TEvent : IDomainEvent
		{
			foreach (var domainEvent in events)
			{
				Dispatch(domainEvent);
			}
		}

		private void Dispatch<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
		{
			var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
			var registerTypes = Assembly.GetExecutingAssembly().GetTypes()
				.Where(type => type.IsClass && handlerType.IsAssignableFrom(type)).ToArray();
			foreach (var concreteType in registerTypes)
			{
				var handler = Activator.CreateInstance(concreteType);
				var handleMethod = concreteType.GetMethod("Handle");
				handleMethod.Invoke(handler, new object[] { domainEvent });
			}
		}
	}
}