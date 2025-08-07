using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ATKApp6.Infrastructure.Extensions
{
    public class MyExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MyExceptionHandlerMiddleware> _logger;

        public MyExceptionHandlerMiddleware(RequestDelegate next, ILogger<MyExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // передаем выполнение дальше
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            string message;

            switch (exception)
            {
                case ArgumentNullException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = "Ссылка на null.";
                    break;

                case ArgumentException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = "Неверный запрос.";
                    break;

                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    message = "Доступ запрещен.";
                    break;

                case KeyNotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    message = "Ресурс не найден.";
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = "Произошла внутренняя ошибка.";
                    break;
            }

            // Логируем только ошибки 5xx
            if (statusCode >= 500)
            {
                _logger.LogError(exception, "Произошло исключение: {Message}", exception.Message);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(message);
        }
    }
}
