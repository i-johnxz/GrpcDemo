using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Payments;

namespace SampleProject.Infrastructure.Payments
{
    public class PaymentRepository : IPaymentRepository
    {

        private readonly OrdersContext _context;

        public PaymentRepository(OrdersContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Payment> GetByIdAsync(PaymentId id)
        {
            return await this._context.Payments.AsNoTracking().SingleAsync(x => x.Id == id);
        }

        public async Task AddAsync(Payment payment)
        {
            await this._context.Payments.AddAsync(payment);
        }
    }
}