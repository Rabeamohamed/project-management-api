
using FluentValidation;
using ProjectManagement.Application.Common.Models;
using System.Net;
using System.Text.Json;

namespace ProjectManagement.API.Middleware
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
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context,ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
            context.Response.ContentType ="application/json";
            var response =new ApiResponse<object>();

            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode =(int)HttpStatusCode.BadRequest;
                    response = ApiResponse<object>.FailureResponse(validationException.Errors
                            .Select(x => x.ErrorMessage), "Validation Failed");
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = ApiResponse<object>.FailureResponse(new[] { exception.Message }, "Unauthorized");
                    
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode =(int)HttpStatusCode.NotFound;
                    response =ApiResponse<object>.FailureResponse(new[]{exception.Message},"Not Found");

                    break;

                default:

                    context.Response.StatusCode =(int)HttpStatusCode.InternalServerError;
                    response =ApiResponse<object>.FailureResponse(new[]{exception.Message}, "Server Error");

                    break;
            }
            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}