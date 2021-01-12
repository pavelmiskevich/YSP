using System;
using System.Linq;
using YSP.Core.Models;
using YSP.Data;

namespace CustomerApi.Data.Test.Infrastructure
{
    public class DatabaseInitializer
    {
        public static void Initialize(YSPDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(YSPDbContext context)
        {
            var categories = new[]
            {
                new Category
                {
                    Name = "Category1"                    
                },
                new Category
                {
                    Name = "Category2"
                },
                new Category
                {
                    Name = "Category3",
                    ParentId = 1
                }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}