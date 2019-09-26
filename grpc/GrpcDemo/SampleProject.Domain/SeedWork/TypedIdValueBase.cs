using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Domain.SeedWork
{
    public abstract class TypedIdValueBase : IEquatable<TypedIdValueBase>
    {
        public Guid Value { get; }

        protected TypedIdValueBase(Guid value)
        {
            Value = value;
        }

        public bool Equals(TypedIdValueBase other)
        {
            return this.Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypedIdValueBase) obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
