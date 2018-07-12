using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapPing.Geolocation.IPLocationServices;
using MapPing.Geolocation.IPLocationServices.IPStack;
using MapPingCore.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MapPingCore
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
            ConfigureDependencies(services);

            

            services.AddCors(opts =>
                opts.AddPolicy("AllowSubdomains", builder =>
                    builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithOrigins(Configuration["CORS:Origin"])));

            services.AddMvc();
            services.AddSignalR();
            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "MapPing", Version = "v1" });
            });
        }

        private static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddTransient<MapHubService, MapHubService>();
        }

        private static void ConfigureIPLocationServiceHttpClients(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IPStackLocationService>(client =>
            {
                IPStackLocationService.HttpClientRegistration(client, configuration.Get<ApiConfiguration>());
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            app.UseCors("AllowSubdomains");
            app.UseMvc();

            app.UseSignalR(routes =>
            {
                routes.MapHub<MapHub>("/map");
            }); 

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MapPing V1"));
        }
    }
}
