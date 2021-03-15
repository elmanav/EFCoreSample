using System;

namespace EFCoreSample
{
	public abstract class Entity
	{
		public Entity()
		{
			Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }

		protected bool Equals(Entity other)
		{
			return Id.Equals(other.Id);
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Entity) obj);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(Entity? left, Entity? right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Entity? left, Entity? right)
		{
			return !(left == right);
		}
	}
}