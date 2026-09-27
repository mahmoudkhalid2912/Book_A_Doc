namespace Book_A_Doc.Application.Interfaces;

public interface ITransactionManager
{
    Task<TResponse> ExecuteAsync<TResponse>(
        Func<CancellationToken, Task<TResponse>> operation,
        CancellationToken cancellationToken = default);
}