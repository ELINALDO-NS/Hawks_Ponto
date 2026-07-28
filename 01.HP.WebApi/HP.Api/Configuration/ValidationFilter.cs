using FluentValidation;

namespace HP.Api.Configuration
{
    public class ValidationFilter<T> : IEndpointFilter where T : class
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            // 1. O .NET injeta diretamente o IValidator<T> sem Reflection
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

            if (validator is not null)
            {
                // 2. Pega o DTO já convertido para o tipo correto T
                var entity = context.Arguments.OfType<T>().FirstOrDefault();

                if (entity is not null)
                {
                    var validationResult = await validator.ValidateAsync(entity);

                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }
                }
            }

            return await next(context);
        }
    }
}
