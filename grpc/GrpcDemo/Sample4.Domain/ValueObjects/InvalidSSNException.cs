using BuildingBlocks.Domain;

namespace Sample4.Domain.ValueObjects
{
    internal sealed class InvalidSSNException : DomainException
    {
        internal InvalidSSNException(string message) : base(message) { }
    }
}