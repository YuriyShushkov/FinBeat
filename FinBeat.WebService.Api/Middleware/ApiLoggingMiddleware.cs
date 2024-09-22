using FinBeat.Domain.Entities;
using System.Text;
using FinBeat.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using FluentValidation;

namespace FinBeat.WebService.Api.Middleware
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public ApiLoggingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();

            var request = await FormatRequest(context.Request);
            var originalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var errors = ex.Errors.Select(error => new { error.PropertyName, error.ErrorMessage });
                await context.Response.WriteAsJsonAsync(errors);
            }
            finally
            {
                var response = await FormatResponse(context.Response);
                await LogRequestAndResponse(context, request, response);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.Body.Position = 0;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0;

            return bodyAsText;
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }

        private async Task LogRequestAndResponse(HttpContext context, string requestBody, string responseBody)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var apiLogRepository = scope.ServiceProvider.GetRequiredService<IApiLogRepository>();

                var log = new ApiLog
                {
                    RequestPath = context.Request.Path,
                    RequestMethod = context.Request.Method,
                    RequestBody = requestBody,
                    ResponseBody = responseBody,
                    Timestamp = DateTime.UtcNow
                };

                await apiLogRepository.SaveAsync(log);
            }
        }
    }
}
