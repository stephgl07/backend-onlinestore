using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Tranzact.OnlineStore.Api.Middlewares;
using Tranzact.OnlineStore.Application.Handlers;
using Tranzact.OnlineStore.Application.Services.Product;
using Tranzact.OnlineStore.Application.Services.ProductDetails;
using Tranzact.OnlineStore.Domain.Api.ApiService;
using Tranzact.OnlineStore.Domain.Api.AppConfiguration;
using Tranzact.OnlineStore.Domain.Services.Product;
using Tranzact.OnlineStore.Domain.Services.ProductDetails;
using Tranzact.OnlineStore.Domain.Services.UnitOfWork;
using Tranzact.OnlineStore.Infrastructure.Api.ApiService;
using Tranzact.OnlineStore.Infrastructure.Api.AppConfiguration;
using Tranzact.OnlineStore.Infrastructure.Data;
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

        public void ConfigureServices(IServiceCollection services)
        {
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
            services.AddTransient<IApiResponseHandler, ApiResponseHandler>();
            services.AddTransient<ErrorHandlerMiddleware>();
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
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
