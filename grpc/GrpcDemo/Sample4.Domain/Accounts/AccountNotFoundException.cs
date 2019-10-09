using BuildingBlocks.Domain;

namespace Sample4.Domain.Accounts
{
    public class AccountNotFoundException : DomainException
    {
        public AccountNotFoundException(string businessMessage) : base(businessMessage)
        {
        }
    }
}