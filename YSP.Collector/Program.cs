using System;
using System.IO;
using YSP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YSP.Core.Models;
using YSP.Data.Datalnitialization;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using YSP.Services;
using Microsoft.Extensions.DependencyInjection;
using YSP.Core.Services;
using Microsoft.Extensions.Logging;
using YSP.Core;
using System.Text;
using Logger;
using YSP.Core.DTO;
using AutoMapper;
using YSP.Search.Core;
using YSP.Search.Core.Classes;
using YSP.Operations;
using System.Diagnostics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;

namespace YSP.Collector
{
    //TODO: убрать видимость после тестов
    public class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            #region Службы windows на .NET Core
            //https://habr.com/ru/company/microsoft/blog/416443/
            //Cannot start service from the command line or a debugger.  A Windows Service must first be installed and then started with the ServerExplorer, Windows Services Administrative tool or the NET START command.
            //var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            //var pathToContentRoot = Path.GetDirectoryName(pathToExe);

            //var host = WebHost.CreateDefaultBuilder(args)
            //    .UseContentRoot(pathToContentRoot)
            //    .UseStartup<Startup>()
            //    .Build();

            //host.RunAsService();
            //return;
            #endregion Службы windows на .NET Core


            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            //TODO: достестировать отмену операции
            Console.CancelKeyPress += delegate {
                cancelTokenSource.Cancel();
            };

            #region IServiceCollection and Startup
            //https://coderoad.ru/41407221/Startup-cs-в-самодостаточном-консольном-приложении-NET-Core
            IServiceCollection services = new ServiceCollection();
            
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            #endregion IServiceCollection and Startup

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>();
                //.AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogInformation($"Начало работы в процессе с Id {Thread.CurrentThread.ManagedThreadId}");

#if DEBUG
            Datalnitializer.Context = serviceProvider.GetService<YSPDbContext>();            
            await Datalnitializer.RecreateDatabaseAsync();
            //await Datalnitializer.ClearDataAsync();
            await Datalnitializer.InitializeDataAsync();
#endif
            try
            {
                var scheduleOperations = serviceProvider.GetService<ScheduleOperations>();
                bool result = await scheduleOperations.Check(token);
            
                //var scheduleService = serviceProvider.GetService<IScheduleService>();
            

                                
                               
                #region Parallel.ForEach
                //ParallelOptions parOpts = new ParallelOptions();
                //parOpts.CancellationToken = token;
                //parOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
                //Parallel.ForEach(queryRegionDTOs, parOpts, YandexSearch);
                #endregion Parallel.ForEach
                #region Task.WhenAll(tasks)
                //var tasks = new List<Task>();
                //foreach (var item in queryRegionDTOs)
                //{
                //    //tasks.Add(YandexSearch2(item));
                //    tasks.Add(new Task(() =>
                //    {
                //        Thread.Sleep(400);
                //        cancelTokenSource.Cancel();
                //    }));
                //}
                //Console.WriteLine("All loaded");
                //await Task.WhenAll(tasks);
                #endregion Task.WhenAll(tasks)
                

                //var categoryService = serviceProvider.GetService<ICategoryService>();
                //await categoryService.GetAllWithParentCategory();
            }
            catch (Exception ex) { }
            finally
            {
                cancelTokenSource.Dispose();
            }
            logger.LogInformation($"Окончание работы в процессе с Id {Thread.CurrentThread.ManagedThreadId}");
            Console.Read();
            return;
            

            

            //обработка результаток
            //modThreeIsZero = (from num in source.AsParallel().WithCancellation(cancelToken.Token)
            //                  where num % 3 == 0
            //                  orderby num descending
            //                  select num).ToArray();            

            //var unitOfWork = new UnitOfWork(context);
            ////using (var unitOfWork = new UnitOfWork(context))
            ////{
            //await unitOfWork.Categories.AddAsync(new Category { Name = "Category" });
            //await unitOfWork.CommitAsync();
            ////}

            //using (var context = new YSPDbContext(options))
            //{
            //    ////Datalnitializer.RecreateDatabase(context);
            //    Datalnitializer.ClearData(context);
            //    Datalnitializer.InitializeData(context);                

            //    foreach (var user in context.Users)
            //    {
            //        Console.WriteLine($"{user.Name} {user.Password}");
            //    }
            //}

            //TODO: использовать для запросов 
            ////Parallel.ForEach<int>(new List<int>() { 1, 3, 5, 8 }, Factorial2);
            //Parallel.ForEach(new List<string>() { "1", "3", "5", "8" }, WriteToFile);
            //Parallel.Invoke(
            //() =>
            //{
            //                // Найти 10 наиболее часто встречающихся слов. 
            //                tenMostCommon = FindTenMostCommon(words);
            //            },
            //() =>
            //// Найти самое длинное слово. 
            //longestWord = FindLongestWord(words);
            //        }) ;

            new Task(() =>
            {
                Thread.Sleep(400);
                cancelTokenSource.Cancel();
            }).Start();

            try
            {
                ParallelOptions parOpts = new ParallelOptions();
                parOpts.CancellationToken = token;
                parOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
                //Parallel.ForEach(files, parOpts, currentFile =>
                //{
                //    parOpts.CancellationToken.ThrowIfCancellationRequested();
                //      https://www.diogonunes.com/blog/webclient-vs-httpclient-vs-httpwebrequest/
                //TODO: использовать HttpClient, но нужно понять как передавать заголовок
                //HttpClient hc = new HttpClient();
                //await hc.GetStringAsync("https://yandex.ru/search/xml?user=pmiskevich&key=03.23131956:c3a7cca8fc4d8c2716b397f3b645a312&query=%D0%9C%D0%B8%D1%81%D0%BA%D0%B5%D0%B2%D0%B8%D1%87&l10n=ru&sortby=tm.order%3Dascending&filter=strict&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D10.docs-in-group%3D1");

                //WebClient wc = new WebClient();
                //wc.DownloadStringCompleted += (s, eArgs) =>
                //{
                //    theEBook = eArgs.Result;
                //    Console.WriteLine(" Download complete. ");
                //    GetStats();
                //};
                //// Загрузить электронную книгу Чарльза Диккенса "A Tale of Two Cities".
                //// Может понадобиться двукратное выполнение этого кода, если ранее вы 
                ////не посещали данный сайт, поскольку при первом его посещении появляется 
                //// окно с сообщением, предотвращающее нормальное выполнение кода.
                //wc.DownloadStringAsync(new Uri("http: //www.gutenberg.org/files/98/98-8 . txt"));
                
                //    string filename = Path.GetFileName(currentFile);
                //    //сделать запрос и сохранить ответ
                //});
            }
            catch (OperationCanceledException ex)
            {
                //this.Invoke((Action)delegate
                //{
                //    this.Title = ex.Message;
                //)) ;
            }
            finally
            {
                cancelTokenSource.Dispose();
            }
        }

        //private static async Task YandexSearchAsync(QueryRegionDTO queryRegionDTO)        
        private static void YandexSearch(QueryRegionDTO queryRegionDTO)
        {
            YandexSearchQuery _query = null;
            List<YaSearchResult> resultList = null;

            //TODO: подумать как передать учетные данные пользователя
            APICredentials _DefaultCredential = new APICredentials();
            _DefaultCredential.User = "pmiskevich";
            _DefaultCredential.Key = "03.23131956:c3a7cca8fc4d8c2716b397f3b645a312";
            
            try
            {
                ////Console.WriteLine(tuple.Item2.Name + ": " + tuple.Item1 + " " + DateTime.Now.TimeOfDay);
                //_query = new YandexSearchQuery(queryRegionDTO.Name, _DefaultCredential, new YandexRegion() { StringName = queryRegionDTO.Region.Name }, RequestMethodEnum.GET);
                //resultList = _query.GetResponseToList();

                Console.WriteLine($"Выполнено {queryRegionDTO.QueryName} {queryRegionDTO.Region.Name} в процессе {Thread.CurrentThread.ManagedThreadId}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        private static async Task YandexSearch2(QueryRegionDTO queryRegionDTO)
        {
            YandexSearchQuery _query = null;
            List<YaSearchResult> resultList = null;

            //TODO: подумать как передать учетные данные пользователя
            APICredentials _DefaultCredential = new APICredentials();
            _DefaultCredential.User = "pmiskevich";
            _DefaultCredential.Key = "03.23131956:c3a7cca8fc4d8c2716b397f3b645a312";

            try
            {
                //////Console.WriteLine(tuple.Item2.Name + ": " + tuple.Item1 + " " + DateTime.Now.TimeOfDay);
                //_query = new YandexSearchQuery(queryRegionDTO.Name, _DefaultCredential, new YandexRegion() { StringName = queryRegionDTO.Region.Name }, RequestMethodEnum.GET);
                //resultList = await _query.GetResponseToListAsync();
                //var ysr = resultList.FirstOrDefault(x => x.Domain == queryRegionDTO.Url);
                //if(!ysr.Equals(default(YaSearchResult)))
                //{
                //    //TODO: додумать сохранение позиций
                //    //var scheduleService = serviceProvider.GetService<IPositionService>();
                //}

                Console.WriteLine($"Выполнено {queryRegionDTO.QueryName} {queryRegionDTO.Region.Name} в процессе {Thread.CurrentThread.ManagedThreadId}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        static async Task<string> DoWorkAsync()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(5_000);
                return "Done with work!";
            });
        }

        static async Task AddCategory(YSPDbContext context)
        {
            var unitOfWork = new UnitOfWork(context);
            //using (var unitOfWork = new UnitOfWork(context))
            //{
                await unitOfWork.Categories.AddAsync(new Category { Name = "Category" });
                await unitOfWork.CommitAsync();
            //}
        }
    }
}
