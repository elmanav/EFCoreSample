namespace EFCoreSample
{
	public class Aircraft : Entity
	{
		/// <inheritdoc />
		public Aircraft(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}
}