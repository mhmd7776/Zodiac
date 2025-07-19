using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    public class ValidationBehavior<TCommand, TResult>(IEnumerable<IValidator<TCommand>> validators) : IPipelineBehavior<TCommand, TResult> where TCommand: notnull
    {
        public async Task<TResult> Handle(TCommand request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TCommand>(request);
            var validationResults = await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

            var errors = validationResults.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors).ToList();
            if (errors.Count > 0)
                throw new ValidationException(errors);

            return await next(cancellationToken);
        }
    }
}
