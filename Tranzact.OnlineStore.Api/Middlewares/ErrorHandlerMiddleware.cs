using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using Tranzact.OnlineStore.Domain.Models.Exceptions.Api;
using Tranzact.OnlineStore.Domain.Models.Exceptions.Core.Business;
using Tranzact.OnlineStore.Domain.Api.Wrappers;
using System.Diagnostics;
using Tranzact.OnlineStore.Domain.Services.Logger;
using Tranzact.OnlineStore.Domain.Models.Logger;

namespace Tranzact.OnlineStore.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        private string _tiempoEjecucionSec = string.Empty;

        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await _next(context);
                stopwatch.Stop();
                _tiempoEjecucionSec = $"{stopwatch.ElapsedMilliseconds} ms";
                _loggerService.InsertLog("Information", "Operación exitosa"); // Registrar log de éxito
                _ = _loggerService.InsertBlobLogInfo(new BlobLogInfo()
                {
                    DateRequest = DateTime.Now,
                    Endpoint = context.Request.Path,
                    ResponseCode = context.Response.StatusCode.ToString(),
                    TimeRequest = _tiempoEjecucionSec
                });
            }
            catch (Exception error)
            {
                stopwatch.Stop();
                _tiempoEjecucionSec = $"{stopwatch.ElapsedMilliseconds} ms";

                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ApiResponse<string>() { Succeeded = false, Message = error?.Message ?? "Message not caught." };


                switch (error)
                {
                    case ApiException e:
                        // custom application error
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        _loggerService.InsertLog("Error", error.Message); // Registrar log de error
                        break;

                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = StatusCodes.Status404NotFound;
                        _loggerService.InsertLog("Error", error.Message); // Registrar log de error
                        break;

                    case BusinessException e:
                        // not found error
                        response.StatusCode = StatusCodes.Status406NotAcceptable;
                        _loggerService.InsertLog("Error", error.Message); // Registrar log de error
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        _loggerService.InsertLog("Error", error.Message); // Registrar log de error
                        break;
                }

                _ = _loggerService.InsertBlobLogInfo(new BlobLogInfo()
                {
                    DateRequest = DateTime.Now,
                    Endpoint = context.Request.Path,
                    ResponseCode = response.StatusCode.ToString(),
                    TimeRequest = _tiempoEjecucionSec
                });

                await response.WriteAsync(JsonConvert.SerializeObject(responseModel, new JsonSerializerSettings()
                {
                    Culture = System.Globalization.CultureInfo.CurrentCulture,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local,
                    FloatFormatHandling = FloatFormatHandling.DefaultValue,
                    FloatParseHandling = FloatParseHandling.Decimal,
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
            }
        }
    }

}
