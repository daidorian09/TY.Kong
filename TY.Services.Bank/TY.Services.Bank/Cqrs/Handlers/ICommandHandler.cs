using System.Threading;
using System.Threading.Tasks;

namespace TY.Services.Bank.Cqrs.Handlers
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}