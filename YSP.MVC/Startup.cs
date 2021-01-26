using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using YSP.Api.Infrastucture;
using YSP.Core;
using YSP.Core.Services;
using YSP.Data;
using YSP.MVC.Extensions;
using YSP.MVC.Middleware;
using YSP.Services;

namespace YSP.MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    options.HttpsPort = 4431;
            //});

            //services.AddHsts(options =>
            //{
            //    options.Preload = true;
            //    options.IncludeSubDomains = true;
            //    options.MaxAge = TimeSpan.FromDays(60);
            //    options.ExcludedHosts.Add("us.example.com");
            //    options.ExcludedHosts.Add("www.example.com");
            //});

            #region 4 Session
            //services.AddDistributedMemoryCache();
            ////services.AddSession();
            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = ".MyApp.Session";
            //    options.IdleTimeout = TimeSpan.FromSeconds(3600);
            //    options.Cookie.IsEssential = true;
            //});
            #endregion 4 Session

            services.AddControllersWithViews();

            services.AddDbContextPool<YSPDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("YSP.Data")));

            //services.AddYSPServices();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IMessageSender, EmailMessageSender>();
            services.AddTransient<IMessageSender, SmsMessageSender>();

            //services.AddScoped<IWaitToFinishMemoryCache<>, WaitToFinishMemoryCache<>>();
            var allCommandHandler = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                t.IsAssignableFrom(typeof(IWaitToFinishMemoryCache<>)));
            foreach (var type in allCommandHandler)
            {
                var allInterfaces = type.GetInterfaces();
                var mainInterfaces = allInterfaces.Where(t => t.IsAssignableFrom(typeof(IWaitToFinishMemoryCache<>)));
                foreach (var itype in mainInterfaces)
                {
                    services.AddScoped(itype, type);
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMessageSender messageSender)
        {
            //env.EnvironmentName = "Production";
            if (env.IsDevelopment())
            {
                //var connectionStrings = Configuration.GetConnectionString("DefaultConnection");
                //var logging = Configuration.GetSection("Logging");

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            
            ////app.UseStatusCodePages("text/plain", "Error. Status code : {0}"); // обработка ошибок HTTP
            ////app.UseStatusCodePagesWithRedirects("/error?code={0}");
            //app.UseStatusCodePagesWithReExecute("/error", "?code={0}");
            //app.Map("/error", ap => ap.Run(async context =>
            //{
            //    await context.Response.WriteAsync($"Err: {context.Request.Query["code"]}");
            //}));
            //app.Map("/hello", ap => ap.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"Hello ASP.NET Core");
            //}));

            //app.Map("/Home/Error", ap => ap.Run(async context =>
            //{
            //    await context.Response.WriteAsync("DivideByZeroException occured!");
            //}));
            //app.Run(async (context) =>
            //{
            //    int x = 0;
            //    int y = 8 / x;
            //    await context.Response.WriteAsync($"Result = {y}");
            //});

            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear(); // удаляем имена файлов по умолчанию
            //options.DefaultFileNames.Add("hello.html"); // добавляем новое имя файла
            //app.UseDefaultFiles(options);
            //app.UseDirectoryBrowser(); //просматривать содержимое каталогов
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\lib")),

            //    RequestPath = new PathString("/pages")
            //});
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthorization();

            ////app.UseMiddleware<TokenMiddleware>();
            //app.UseToken("555555");

            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";

            //    if (env.IsEnvironment("Test")) // Если проект в состоянии "Test"
            //    {
            //        await context.Response.WriteAsync("В состоянии тестирования");
            //    }
            //    else if (env.IsEnvironment("Development"))
            //    {
            //        await context.Response.WriteAsync("В процессе разработки");
            //    }
            //    else
            //    {
            //        await context.Response.WriteAsync("В продакшене");
            //    }

            //    await next.Invoke();
            //});

            //app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseMiddleware<AuthenticationMiddleware>();
            //app.UseMiddleware<RoutingMiddleware>();

            #region HttpContext.Items
            //app.Use(async (context, next) =>
            //{
            //    context.Items["text"] = "Text from HttpContext.Items";
            //    await next.Invoke();
            //});

            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/html; charset=utf-8";
            //    if(context.Items.ContainsKey("text1"))
            //        await context.Response.WriteAsync($"Текст: {context.Items["text"]}");
            //    else
            //        await context.Response.WriteAsync($"Нет текста в context.Items");
            //});
            #endregion HttpContext.Items

            #region Request.Cookies
            //app.Run(async (context) =>
            //{
            //    if (context.Request.Cookies.ContainsKey("name"))
            //    {
            //        string name = context.Request.Cookies["name"];
            //        await context.Response.WriteAsync($"Hello {name}!");
            //    }
            //    else
            //    {
            //        context.Response.Cookies.Append("name", "Tom");
            //        await context.Response.WriteAsync("Hello World!");
            //    }
            //});
            #endregion Request.Cookies

            #region 4 Session
            //app.UseSession();
            ////app.Run(async (context) =>
            ////{
            ////    if (context.Session.Keys.Contains("name"))
            ////        await context.Response.WriteAsync($"Hello {context.Session.GetString("name")}!");
            ////    else
            ////    {
            ////        context.Session.SetString("name", "Tom");
            ////        await context.Response.WriteAsync("Hello World!");
            ////    }
            ////});
            //app.Use(async (context, next) =>
            //{
            //    if (context.Session.Keys.Contains("person"))
            //    {
            //        Person person = context.Session.Get<Person>("person");
            //        await context.Response.WriteAsync($"Hello {person.Name}, your age: {person.Age}!");
            //    }
            //    else
            //    {
            //        Person person = new Person { Name = "Tom", Age = 22 };
            //        context.Session.Set<Person>("person", person);
            //        await context.Response.WriteAsync("Hello World!");
            //    }
            //    await next.Invoke();
            //});
            #endregion 4 Session

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(messageSender.Send());
            //});

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync($"Application Name: {Environment.ApplicationName}");
                //});

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                    //pattern: "{controller=Category}/{action=Index}/{id?}");
            });            
        }
    }

    public interface IMessageSender
    {
        string Send();
    }
    public class EmailMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Sent by Email";
        }
    }

    public class SmsMessageSender : IMessageSender
    {
        public string Send()
        {
            return "SMS by Email";
        }
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
