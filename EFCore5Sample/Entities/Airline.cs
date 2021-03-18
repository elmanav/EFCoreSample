using System.Collections.Generic;

namespace EFCoreSample.Entities
{
	public class Airline
	{
		public Airline(string name)
		{

		}

		public IReadOnlyCollection<Aircraft> Fleet { get; }
	}


	public class Flight
	{
		public Flight(string number, Airline airline, Aircraft craft)
		{
		}

		public bool Completed { get; }
	}
}