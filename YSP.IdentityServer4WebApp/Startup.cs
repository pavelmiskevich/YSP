using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YSP.IdentityServer4WebApp.Data;
using YSP.IdentityServer4WebApp.Extensions;
using YSP.IdentityServer4WebApp.Models;

namespace YSP.IdentityServer4WebApp
{
    //TODO: проверить метод ниже
    //https://docs.microsoft.com/ru-ru/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-3.1
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
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //https://stackoverflow.com/questions/56802617/how-to-set-password-options-in-identity-server-4
            services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    //options.SignIn.RequireConfirmedEmail = true; 

                    //User validator
                    options.User.RequireUniqueEmail = true;

                    //Password Validator
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureNonBreakingSameSiteCookies();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
            //    {
            //        var apiResource = options.ApiResources.First();
            //        apiResource.UserClaims = new[] { "hasUsersGroup" };

            //        var identityResource = new IdentityResource
            //        {
            //            Name = "customprofile",
            //            DisplayName = "Custom profile",
            //            UserClaims = new[] { "hasUsersGroup" },
            //        };
            //        identityResource.Properties.Add(ApplicationProfilesPropertyNames.Clients, "*");
            //        options.IdentityResources.Add(identityResource);
            //    });

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddControllers();
            services.AddRazorPages();
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

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
