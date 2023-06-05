using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.AzureAppServices;
using Tranzact.OnlineStore.Api.Middlewares;
using Tranzact.OnlineStore.Application.Handlers;
using Tranzact.OnlineStore.Application.Services.Logger;
using Tranzact.OnlineStore.Application.Services.Product;
using Tranzact.OnlineStore.Application.Services.ProductDetails;
using Tranzact.OnlineStore.Domain.Api;
using Tranzact.OnlineStore.Domain.Api.ApiService;
using Tranzact.OnlineStore.Domain.Api.AppConfiguration;
using Tranzact.OnlineStore.Domain.FileStorage;
using Tranzact.OnlineStore.Domain.Services.Logger;
using Tranzact.OnlineStore.Domain.Services.Product;
using Tranzact.OnlineStore.Domain.Services.ProductDetails;
using Tranzact.OnlineStore.Domain.Services.UnitOfWork;
using Tranzact.OnlineStore.Infrastructure.Api;
using Tranzact.OnlineStore.Infrastructure.Api.ApiService;
using Tranzact.OnlineStore.Infrastructure.Api.AppConfiguration;
using Tranzact.OnlineStore.Infrastructure.Data;
using Tranzact.OnlineStore.Infrastructure.FileStorage;
using Tranzact.OnlineStore.Infrastructure.Repositories.UnitOfWork;

namespace Tranzact.OnlineStore.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string corsApp = "tranzact.webapi.onlinestore";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureLogging(ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.AddAzureWebAppDiagnostics();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AzureFileLoggerOptions>(options =>
            {
                options.FileName = "application-log.txt";
                options.FileSizeLimit = 50 * 1024;
                options.RetainedFileCountLimit = 5;
            });

            services.Configure<AzureBlobLoggerOptions>(options =>
            {
                options.BlobName = "log.txt";
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: corsApp,
                    builder =>
                    {
                        builder.AllowAnyMethod()
                                .AllowAnyHeader()
                                .SetIsOriginAllowed((host) => true)
                                .AllowCredentials();
                    });
            });

            services.AddSingleton(Configuration);
            services.AddSingleton<IAppConfiguration, AppConfiguration>();

            // Configuración de las cadenas de conexión
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<DBOnlineStoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DB_OnlineStore"))
            );

            services.AddControllers();
            services.AddHttpClient();

            // Inyección de dependencias
            services.AddTransient<IApiService, ApiService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductDetailService, ProductDetailService>();
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddTransient<IFileStorage, AzureBlobStorage>();
            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();
            services.AddTransient<IApiResponseHandler, ApiResponseHandler>();
            services.AddTransient<ErrorHandlerMiddleware>(next => new ErrorHandlerMiddleware(next.GetRequiredService<RequestDelegate>(), next.GetRequiredService<ILoggerService>()));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(corsApp);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Enotria.CargaJrlEdi.Api"));

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseMiddleware<ErrorHandlerMiddleware>(app.ApplicationServices.GetRequiredService<ILoggerService>());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
