using FluentValidation;
using FluentValidation.Results;

namespace OpenSkinsApi.Infrastructure.Http.Filters
{
    public class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

            if (validator is not null)
            {
                var entity = context.Arguments.OfType<T>().FirstOrDefault(a => a?.GetType() == typeof(T));
                if (entity is not null)
                {
                    var result = await validator.ValidateAsync(entity);
                    var errors = result.Errors
                        .Select(x => new ValidationFailure(x.PropertyName, x.ErrorMessage)).ToList();

                    if (errors.Any())
                    {
                        throw new ValidationException(errors);
                    }
                }
                else
                {
                    throw new ValidationException("Unkonwn error ocurred");
                }
            }

            return await next(context);
        }
    }
}