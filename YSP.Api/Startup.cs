using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using YSP.Api.Extensions;
using YSP.Core;
using YSP.Data;
//using Swashbuckle.AspNetCore.Swagger;

namespace YSP.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //https://docs.microsoft.com/ru-ru/aspnet/core/security/cors?view=aspnetcore-3.1
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: MyAllowSpecificOrigins,
            //        builder =>
            //        {
            //            builder.WithOrigins("http://localhost:3000/",
            //                                "http://www.contoso.com");
            //        });
            //});

            //https://www.codeproject.com/Questions/5162494/Currently-I-am-working-on-angular-and-web-API-NET
            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins("http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
            });
            //services.AddAuthentication(IISDefaults.AuthenticationScheme);

            //https://stackoverflow.com/questions/57626878/the-json-value-could-not-be-converted-to-system-int32
            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<YSPDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("YSP.Data")));

            services.AddYSPServices();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddLogging();

            //TODO: https://stackoverflow.com/questions/43447688/setting-up-swagger-asp-net-core-using-the-authorization-headers-bearer            
            services.AddSwaggerGen(options =>
            {
                //TODO: https://stackoverflow.com/questions/46764769/swagger-web-api-optional-query-parameters
                //options.OperationFilter<Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter>();

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Yandex Site Positions", Version = "v1" });
            });

            //это если без Resources и с пакетом Microsoft.AspNetCore.Mvc.NewtonsoftJson
            //services.AddControllersWithViews()
            //    .AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);            

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors(MyAllowSpecificOrigins);
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yandex Site Positions V1");
            });
        }
    }
}
