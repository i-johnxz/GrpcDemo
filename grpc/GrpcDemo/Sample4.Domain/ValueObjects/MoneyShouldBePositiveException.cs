using BuildingBlocks.Domain;

namespace Sample4.Domain.ValueObjects
{
    public sealed class MoneyShouldBePositiveException : DomainException
    {
        internal MoneyShouldBePositiveException(string message) : base(message) { }
    }
}