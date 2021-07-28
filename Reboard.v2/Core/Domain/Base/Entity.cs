using System;

namespace Reboard.Core.Domain.Base
{
    public class Entity<TId>
        where TId : OneValueObject<Guid>
    {
        public TId Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var entity = (Entity<TId>)obj;

            return Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Id.GetHashCode();
                return hash;
            }
        }
    }
}