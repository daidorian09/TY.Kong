using System.Threading;
using System.Threading.Tasks;

namespace TY.Services.Bank.Cqrs.Handlers
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}