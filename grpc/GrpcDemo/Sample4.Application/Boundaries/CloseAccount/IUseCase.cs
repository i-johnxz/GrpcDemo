using System.Threading.Tasks;

namespace Sample4.Application.CloseAccount
{
    public interface IUseCase
    {
        Task Execute(CloseAccountInput closeAccountInput);
    }
}