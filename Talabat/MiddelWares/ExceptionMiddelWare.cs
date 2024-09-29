using System.Net;
using System.Text.Json;
using Talabat.Errors;

namespace Talabat.MiddelWares
{
    public class ExceptionMiddelWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddelWare> _loggerfactory;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddelWare(RequestDelegate next,ILogger<ExceptionMiddelWare> loggerfactory,IWebHostEnvironment env)
        {
           _next = next;
           _loggerfactory = loggerfactory;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

            }
            catch (Exception ex)
            {
                _loggerfactory.LogError(ex.Message);
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var response = _env.IsDevelopment() ?
                    new ApiExceptionError((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                    new ApiExceptionError((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await httpContext.Response.WriteAsync(json);
            }

        }
    }
}
