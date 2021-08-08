using System;

namespace Reboard.Core.Domain.Base
{
    public abstract class OneValueObject<TValue> : IEquatable<OneValueObject<TValue>> where TValue : IEquatable<TValue>
    {
        public TValue Value { get; }

        protected OneValueObject(TValue value)
        {
            Value = value;
        }

        public static bool operator !=(OneValueObject<TValue> x, OneValueObject<TValue> y)
        {
            return !(x == y);
        }

        public static bool operator ==(OneValueObject<TValue> obj1, OneValueObject<TValue> obj2)
        {
            if (object.Equals(obj1, null))
            {
                if (object.Equals(obj2, null))
                {
                    return true;
                }
                return false;
            }
            return obj1.Equals(obj2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is OneValueObject<TValue> other && Equals(other);
        }

        public bool Equals(OneValueObject<TValue> other)
        {
            return this.Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}