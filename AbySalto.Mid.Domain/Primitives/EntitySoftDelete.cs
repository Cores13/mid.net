namespace AbySalto.Mid.Domain.Primitives
{
    public abstract class EntitySoftDelete : IEquatable<EntitySoftDelete>
    {
        public int? Id { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public DateTime? UpdatedOn { get; set; } = DateTime.Now;

        public DateTime? DeletedOn { get; set; }

        public static bool operator ==(EntitySoftDelete? first, EntitySoftDelete? second)
        {
            return first is not null && second is not null && first.Equals(second);
        }

        public static bool operator !=(EntitySoftDelete? first, EntitySoftDelete? second)
        {
            return !(first == second);
        }

        public bool Equals(EntitySoftDelete? other)
        {
            if (other is null)
            {
                return false;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return other.Id == Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            if (obj is not EntitySoftDelete entity)
            {
                return false;
            }

            return entity.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}