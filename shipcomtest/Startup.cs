using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using shipcomtest.Models;

namespace shipcomtest
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
         services.AddDbContext<ShipcomDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ShipcomConnection")));
         services.AddScoped<IOhmValueCalculator, OhmValueCalculatorService>();
         services.AddControllersWithViews();

         // In production, the React files will be served from this directory
         services.AddSpaStaticFiles(configuration =>
         {
            configuration.RootPath = "ClientApp/build";
         });
		 
         var corsBuilder = new CorsPolicyBuilder();
         corsBuilder.AllowAnyHeader();
         corsBuilder.AllowAnyMethod();
         corsBuilder.AllowAnyOrigin(); 

         corsBuilder.AllowCredentials();

         services.AddCors(o => o.AddPolicy("SiteCorsPolicy", builder =>
         {
            builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
         }));

      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         app.UseHttpsRedirection();
         app.UseStaticFiles();
         app.UseSpaStaticFiles();

         app.UseRouting();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller}/{action=Index}/{id?}");
         });

         app.UseSpa(spa =>
         {
            spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
               spa.UseReactDevelopmentServer(npmScript: "start");
            }
         });
      }
   }
}
