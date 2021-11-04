using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.AspNetCoreWebApi.Middlewares
{
    /// <summary>
    /// 全局自定义异常扩展
    /// </summary>
    public static class CustomerExceptionMiddlewareExtension
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(CustomerExceptionMiddleware));
        }
    }

    /// <summary>
    /// 全局自定义异常中间件
    /// </summary>
    public class CustomerExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomerExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                var problem = new ProblemDetails
                {
                    Status = 200,
                    Title = "请求发生异常（该异常为全局拦截器捕获）",
                    Detail = ex.ToString()
                };

                await JsonSerializer.SerializeAsync(context.Response.Body, problem);
            }
        }
    }
}
