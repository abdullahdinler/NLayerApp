using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;

namespace NLayer.API.Middlewares
{
    #region UseCustomExceptionHandler Info
    // IApplicationBuilder arayüzünü kullanarak UseCustomExceptionHandler adında bir extension method oluşturduk.
    // Bu methodu Program.cs içerisinde UseCustomException(); şeklinde kullanacağız.
    // Bu methodu kullanarak UseExceptionHandler middleware'ini kullanacağız. 
    // UseExceptionHandler middleware'ini kullanarak hata durumlarında istediğimiz şekilde response dönebiliriz.
    #endregion

    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;
                    var response = CustomResponseDto<NoContentDto>.Fail(exceptionFeature.Error.Message, statusCode);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
