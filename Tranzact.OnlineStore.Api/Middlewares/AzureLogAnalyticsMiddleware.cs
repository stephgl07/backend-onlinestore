namespace Tranzact.OnlineStore.Api.Middlewares
{
    public class AzureLogAnalyticsMiddleware : IMiddleware
    {
        private readonly ILogger<AzureLogAnalyticsMiddleware> _logger;

        public AzureLogAnalyticsMiddleware(ILogger<AzureLogAnalyticsMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                // Lógica de tu middleware
                _logger.LogInformation("Ejecución terminada.");
                await next(context);
            }
            catch (Exception error)
            {
                // Manejo de errores

                _logger.LogError(error, "Error en Ejecución");

                throw;
            }
        }
    }
}
