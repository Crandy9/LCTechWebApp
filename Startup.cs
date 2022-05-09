using LCWebApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCWebApp
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
            services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // configure ASP.NET Core Identity using Entity Framework Core
            // using connection string in settings.json
            // options.UseSqlServer configures context to a microsoft SQL server Database
            // (Configuration.GetConnectionString("DefaultConnection"))); is the 
            // connection string which allows the database to connect to this app
            // "DefaultConnection" name can be any string you want
            // connection string is also found in web.config XML file as well
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            //login stuff that I don't know about yet lol
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // external log in Facebook, Twitter, Google, and Microsoft
            // for other external providers in third-party packages, see 
            // https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
            // https://github.com/aspnet-contrib/AspNet.Security.OpenId.Providers
           
        }
            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            /**
             * Used in .NET Core pipeline
             * These methods are middleware required for functionality and displaying info (MVC is an example)
             * influences the response by the application 
             */
            // process through which the application matches the requested URL path and executes the related Controller and Action
            // ASP.NET Core controllers use the routing middleware to match the URLs of incoming requests and map them to appropriate controller and action
            // routes describe how URLs are matched with the action
            // generates URLs for links
            app.UseRouting();
            // enable ASP.NET Core Identity middleware
            app.UseAuthentication();
            app.UseAuthorization();
            //when request comes in from browser to application, request goes through pipeline made up of middleware
            app.UseHttpsRedirection();
            //wwwroot files are made available by UseStaticFiles middleware
            app.UseStaticFiles();

         

            //endpoints are the app's units of executable request-handling code
            //endpoints are defined in the app and configured when the app starts
            //MVC, razor pages, signalR are examples of kinds of endpoints. app.UseRouting will specify kind of endpoint to be used.
            //in this case, MVC endpoint is used
            app.UseEndpoints(endpoints =>
            {
                // enable ASP.Net Core Identity
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
