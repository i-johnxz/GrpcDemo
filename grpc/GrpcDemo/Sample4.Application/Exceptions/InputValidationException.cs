using BuildingBlocks.Domain;

namespace Sample4.Application.Exceptions
{
    public class InputValidationException : DomainException
    {
        public InputValidationException(string businessMessage) : base(businessMessage)
        {
        }
    }
}