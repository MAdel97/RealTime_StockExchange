using RealTime_StockExchange.Interfaces;
using RealTime_StockExchange.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using System;
using RealTime_StockExchange.Helper;
using StockUpdates.Context;
using SignalRDemo.Hub;

namespace RealTime_StockExchange
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
            services.AddSignalR();
            services.AddControllers();
            

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).ConfigureApiBehaviorOptions(options =>
            {
                // Adds a custom error response factory when ModelState is invalid
                //options.InvalidModelStateResponseFactory = InvalidModelStateFilterConvention;

            });
            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);
            services.AddDbContext<StockUpdatesContext>(options =>
                  options.UseSqlServer("Server=Hady-Sharawi\\SQLEXPRESS;Database=ClientStock;Trusted_Connection=True;MultipleActiveResultSets=true"));
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IStockRepository, StockRepository>();
            services.AddHostedService<ConsumeScopedServiceHostedService>();
            services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Frontend";
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
           .AllowAnyHeader()
           .AllowAnyMethod()
           .SetIsOriginAllowed((host) => true)
           .AllowCredentials()
       );


            app.Use((context, next) =>
            {
                context.Items["__CorsMiddlewareInvoked"] = true;
                return next();
            });



            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<MessageHub>("/GetStocks");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Frontend/src";


                if (env.IsDevelopment())
                {

                    //spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    // spa.UseProxyToSpaDevelopmentServer("
                }

            });

        }
    }
}
