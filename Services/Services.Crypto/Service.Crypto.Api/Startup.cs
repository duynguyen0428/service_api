using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Service.Crypto.Data;
using Service.Crypto.Domain;
using Service.Crypto.Application;

namespace Service.Crypto.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
        services.Configure<CryptoDatabaseSettings>(
                Configuration.GetSection(nameof(CryptoDatabaseSettings)));

            services.AddSingleton<ICryptoDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<CryptoDatabaseSettings>>().Value);
            services.AddHostedService<ConsumeRabbitMQHostedService>();
            services.AddControllers();

            services.AddScoped<ICryptoDbContext, CryptoDbContext>();
            services.AddScoped<ICryptoEntity, CryptoEntityRepository>();
            services.AddScoped<ICryptoService, CryptoServiceRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
