using Core.Exceptions.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Core.Exceptions;

public static class ExceptionExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}