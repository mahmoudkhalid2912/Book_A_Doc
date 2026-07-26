using Book_A_Doc.Domain.ResultPattern;
using FluentValidation;
using MediatR;

namespace Book_A_Doc.Application.Behaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        Error[] errors = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .Select(f => new Error(
                f.PropertyName,
                f.ErrorMessage,
                400))
            .Distinct()
            .ToArray();

        if (!errors.Any())
        {
            return await next();
        }

        return CreateValidationResult(errors);
    }

    private static TResponse CreateValidationResult(Error[] errors)
    {
        if (typeof(TResponse) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResponse)!;
        }

        Type valueType = typeof(TResponse).GenericTypeArguments[0];

        Type validationResultType =
            typeof(ValidationResult<>).MakeGenericType(valueType);

        var method = validationResultType.GetMethod(
            nameof(ValidationResult<int>.WithErrors));

        return (TResponse)method!
            .Invoke(null, new object[] { errors })!;
    }
}