using BuildingBlocks.Domain;

namespace Sample4.Domain.Customers
{
    public sealed class CustomerNotFoundException : DomainException
    {
        public CustomerNotFoundException(string message) : base(message) { }
    }
}