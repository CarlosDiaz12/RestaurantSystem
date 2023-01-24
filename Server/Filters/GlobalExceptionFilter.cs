using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ResturantSystem.Application.Exceptions;
using System.Net;
using RestaurantSystem.Shared.Model;

namespace RestaurantSystem.Server.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var statusCode = HttpStatusCode.InternalServerError;
            var message = exception.Message;

            if (exception.GetType() == typeof(BusinessException))
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            var response = new ApiResponse<bool>(false)
            {
                ErrorMessage = message,
                Success = false,
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)statusCode
            };

            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.ExceptionHandled = true;
        }
    }
}
