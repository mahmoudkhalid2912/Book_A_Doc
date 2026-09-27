using MediatR;

namespace Book_A_Doc.Application.Interfaces;

public interface ITransactionalRequest<out TResponse>
    : IRequest<TResponse>
{
}