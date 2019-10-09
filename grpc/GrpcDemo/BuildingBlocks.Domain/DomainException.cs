using System;

namespace BuildingBlocks.Domain
{
    public class DomainException : Exception
    {
        public DomainException(string businessMessage) : base(businessMessage) { }
    }
}