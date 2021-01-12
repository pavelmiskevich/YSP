using Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Security.Cryptography;
using YSP.Core.Models;
using YSP.Data.Configurations;
using YSP.Data.Configurations.BaseConfigurations;

namespace YSP.Data
{
    public class YSPDbContext : DbContext
    {
        // Get key and IV from a Base64String or any other ways.
        // You can generate a key and IV using "AesProvider.GenerateKey()"
        //private readonly byte[] _encryptionKey = new AesCryptoServiceProvider().Key; //AesProvider.GenerateKey(); //AesCryptoServiceProvider.Create().Key;
        //private readonly byte[] _encryptionIV = new AesCryptoServiceProvider().IV;//AesProvider.GenerateIV(); //AesCryptoServiceProvider.Create().IV;
        //private readonly byte[] _encryptionKey = Buffer.BlockCopy(int[], 0, result, 0, result.Length);

        private readonly byte[] _encryptionKey = new byte[] { (byte)161, (byte)82, (byte)40, (byte)75, (byte)68, (byte)7, (byte)87, (byte)5, (byte)121, (byte)176, (byte)55, (byte)198, (byte)67, (byte)182, (byte)109, (byte)225, (byte)236, (byte)179, (byte)2, (byte)65, (byte)98, (byte)0, (byte)46, (byte)60, (byte)117, (byte)166, (byte)68, (byte)53, (byte)231, (byte)24, (byte)188, (byte)116 };
        private readonly byte[] _encryptionIV = new byte[] { (byte)183, (byte)48, (byte)35, (byte)169, (byte)1, (byte)170, (byte)99, (byte)196, (byte)68, (byte)207, (byte)9, (byte)187, (byte)14, (byte)158, (byte)241, (byte)24 };

        private readonly IEncryptionProvider _provider;

        //TODO: понять нужен ли virtual
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Offer> Offers { get; set; }
        //TODO: возможно не нужно добавлять DBSet для Position, так как не планируется извлечение просто позиций
        public DbSet<Position> Positions { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Region> Regions { get; set; }
        //TODO: возможно не нужно добавлять DBSet для Schedule, так как не планируется извлечение просто расписания
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<SystemState> SystemStates { get; set; }
        public DbSet<User> Users { get; set; }

        public YSPDbContext(DbContextOptions<YSPDbContext> options)
            : base(options)
        {
            this._provider = new AesProvider(this._encryptionKey, this._encryptionIV);
        }        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseEncryption(this._provider);

            builder
                .ApplyConfiguration(new CategoryConfiguration());

            builder
                .ApplyConfiguration(new FeedbackConfiguration());

            builder
                .ApplyConfiguration(new OfferConfiguration());

            builder
                .ApplyConfiguration(new PositionConfiguration());

            builder
                .ApplyConfiguration(new QueryConfiguration());

            builder
                .ApplyConfiguration(new RegionConfiguration());

            builder
                .ApplyConfiguration(new ScheduleConfiguration());

            builder
                .ApplyConfiguration(new SiteConfiguration());

            builder
                .ApplyConfiguration(new SystemStateConfiguration());

            builder
                .ApplyConfiguration(new UserConfiguration());
        }

        /// <summary>
        /// Глобальная установка логгирования в контексте данных
        /// https://metanit.com/sharp/entityframeworkcore/2.12.php
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            //MyLoggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logs/", $"{DateTime.Now.ToString("yyyyMMdd")}logs.txt"));
        }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            // или так с более детальной настройкой
            //builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
            //            && level == LogLevel.Information)
            //       .AddConsole();            
        });
    }
}
