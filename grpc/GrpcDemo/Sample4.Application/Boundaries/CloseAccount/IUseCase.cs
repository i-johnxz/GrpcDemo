using System.Threading.Tasks;

namespace Sample4.Application.Boundaries.CloseAccount
{
    public interface IUseCase
    {
        Task Execute(CloseAccountInput closeAccountInput);
    }
}