using System;
using System.Threading.Tasks;

namespace BuildingBlocks.Infrastructure.InternalCommands
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}
