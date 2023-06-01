using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Tranzact.OnlineStore.Application.Services.Product;
using Tranzact.OnlineStore.Domain.Services.Product;
using Tranzact.OnlineStore.Domain.Services.UnitOfWork;
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


            // Configuración de las cadenas de conexión
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<DBProductsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DB_Products_Test"))
            );

            services.AddControllers();

            // Inyección de dependencias
            services.AddTransient<IProductService, ProductService>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(corsApp);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Enotria.CargaJrlEdi.Api"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
