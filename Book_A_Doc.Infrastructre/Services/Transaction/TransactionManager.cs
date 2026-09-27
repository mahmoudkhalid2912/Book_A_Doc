using Book_A_Doc.Application.Interfaces;
using Book_A_Doc.Infrastructre.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Book_A_Doc.Infrastructure.Services;

public class TransactionManager(
    Book_A_Doc_Context context) : ITransactionManager
{
    private readonly Book_A_Doc_Context _context = context;

    public async Task<TResponse> ExecuteAsync<TResponse>(
        Func<CancellationToken, Task<TResponse>> operation,
        CancellationToken cancellationToken = default)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync(
                IsolationLevel.Serializable,
                cancellationToken);

        try
        {
            var response = await operation(cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return response;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}