using System.Threading.Tasks;

namespace Sample4.Application.Boundaries.Deposit
{
    public interface IUseCase
    {
        Task Execute(DepositInput depositInput);
    }
}