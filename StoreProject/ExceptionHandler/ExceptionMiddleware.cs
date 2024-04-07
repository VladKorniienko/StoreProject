using StoreProject.Common.Exceptions;
using Swashbuckle.AspNetCore.Swagger;
using System.Text.Json;

namespace StoreProject.ExceptionHandler
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Customize the response based on the exception
            context.Response.ContentType = "application/json";
            context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            var response = new
            {
                error = exception.Message
                //stack trace for debugging
                //,stackTrace = exception.StackTrace
            };

            switch(exception)
            {
                case BadHttpRequestException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case ArgumentException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case InvalidOperationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
