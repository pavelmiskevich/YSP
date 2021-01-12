using System;
using Microsoft.EntityFrameworkCore;
using YSP.Data;

namespace CustomerApi.Data.Test.Infrastructure
{
    public class DatabaseTestBase : IDisposable
    {
        protected readonly YSPDbContext Context;
        protected readonly DbContextOptions<YSPDbContext> options;

        public DatabaseTestBase()
        {
            //var options = new DbContextOptionsBuilder<YSPDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            options = new DbContextOptionsBuilder<YSPDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            Context = new YSPDbContext(options);

            Context.Database.EnsureCreated();

            DatabaseInitializer.Initialize(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}