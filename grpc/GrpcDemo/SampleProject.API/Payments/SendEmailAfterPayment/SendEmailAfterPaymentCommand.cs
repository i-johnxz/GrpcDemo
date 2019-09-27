using MediatR;
using SampleProject.Domain.Payments;

namespace SampleProject.API.Payments.SendEmailAfterPayment
{
    public class SendEmailAfterPaymentCommand : IRequest
    {
        public PaymentId PaymentId { get;  }

        public SendEmailAfterPaymentCommand(PaymentId paymentId)
        {
            PaymentId = paymentId;
        }
    }
}