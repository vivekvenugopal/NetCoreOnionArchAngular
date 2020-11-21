using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Demo.DAL;
using Demo.Common;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
    public class DemoDbContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
    {
        public DemoDbContext CreateDbContext(string[] args)
        {
              IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string ConnectionString = configuration.GetConnectionString("DemoDbContext");
            var optionsBuilder = new DbContextOptionsBuilder<DemoDbContext>();
            optionsBuilder.
            UseMySQL(ConnectionString
            , b => b.MigrationsAssembly("Migrations"));

            return new DemoDbContext(optionsBuilder.Options);
        }
    }
}
