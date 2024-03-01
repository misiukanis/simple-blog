using FluentValidation;

namespace Blog.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline.
                await _next(context);
            }
            catch (ValidationException)
            {
                var response = new
                {
                    type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1",
                    title = "Validation error",
                    status = StatusCodes.Status400BadRequest,
                    detail = "One or more validation errors occurred."
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
