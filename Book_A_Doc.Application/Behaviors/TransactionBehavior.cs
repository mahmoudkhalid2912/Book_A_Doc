using Book_A_Doc.Application.Interfaces;
using MediatR;

namespace Book_A_Doc.Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse>(
    ITransactionManager transactionManager)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ITransactionalRequest<TResponse>)
        {
            return await next(cancellationToken);
        }

        return await transactionManager.ExecuteAsync(
            ct => next(ct),
            cancellationToken);
    }
}