using System;
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
            int statusCode;
            string message;

            try
            {
                await _next(context); // передаем выполнение дальше
            }
            catch (ArgumentNullException ex)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "Ссылка на null.";
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(message);
            }
            catch (ArgumentException ex)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "Неверный запрос.";
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(message);
            }
            catch (UnauthorizedAccessException ex)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = "Доступ запрещен.";
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(message);
            }
            catch (KeyNotFoundException ex)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                message = "Ресурс не найден.";
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(message);
            }
            //catch (Exception ex)
            //{
            //    statusCode = (int)HttpStatusCode.InternalServerError;
            //    message = "Произошла внутренняя ошибка.";

            //    if (statusCode >= 500)
            //    {
            //        _logger.LogError(ex, "Произошло исключение: {Message}", ex.Message);
            //    }

            //    context.Response.ContentType = "application/json";
            //    context.Response.StatusCode = statusCode;

            //    await context.Response.WriteAsync(message);
            //}
        }
    }
}
