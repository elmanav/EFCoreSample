using System.Collections.Generic;

namespace EFCoreSample.Entities
{
	public class AircraftType : Entity
	{
		private readonly Dictionary<string, object> _data = new Dictionary<string, object>();
		
		public AircraftType(string name)
		{
			Name = name;
		}

		public object this[string index]
		{
			get => _data[index];
			set => _data[index] = value;
		}

		public string Name { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Type: {Name} - Vendor: {this["Vendor"]}; MaxTOW: {this["MaxTOW"]}";
		}
	}
}