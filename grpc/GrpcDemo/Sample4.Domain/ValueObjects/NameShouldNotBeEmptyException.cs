using BuildingBlocks.Domain;

namespace Sample4.Domain.ValueObjects
{
    public sealed class NameShouldNotBeEmptyException : DomainException
    {
        internal NameShouldNotBeEmptyException(string message) : base(message) { }
    }
}