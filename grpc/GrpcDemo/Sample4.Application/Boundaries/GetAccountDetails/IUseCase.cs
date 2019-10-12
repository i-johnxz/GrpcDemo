using System.Threading.Tasks;

namespace Sample4.Application.Boundaries.GetAccountDetails
{
    public interface IUseCase
    {
        Task Execute(GetAccountDetailsInput getAccountDetailsInput);
    }
}