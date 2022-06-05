using Common;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace UserManagementApi.Middleware
{
    public class ErrorHandling
    {
        private readonly RequestDelegate _next;

        public ErrorHandling(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception ex)
        {
            var exceptionType = ex.GetType();
            context.Response.ContentType = "application/json";

            var response = exceptionType switch
            {
                Type t when t == typeof(ArgumentNullException) => BaseResponse<string>.BadRequest(),
                Type t when t == typeof(NotFoundException) => BaseResponse<string>.NotFound(),
                _ => BaseResponse<string>.InternalServerError()
            };

            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsync(SerializeResponse(response));
        }

        private static string SerializeResponse<T>(BaseResponse<T> response) where T: class =>
            JsonConvert.SerializeObject(response);
    }
}
