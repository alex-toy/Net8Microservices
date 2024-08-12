using BuildingBlocs.CQRS;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BuildingBlocs.Behaviors;

public class Validator<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
        IEnumerable<Task<ValidationResult>> validationResultsTasks = validators.Select(v => v.ValidateAsync(context, cancellationToken));
        ValidationResult[] validationResults = await Task.WhenAll(validationResultsTasks);
        List<ValidationFailure> failures = validationResults.Where(r => r.Errors.Any()).SelectMany(r => r.Errors).ToList();

        if (failures.Any()) throw new ValidationException(failures);

        return await next();
    }
}
