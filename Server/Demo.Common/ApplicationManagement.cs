using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Demo.Common
{
    public static class ApplicationManagement
    {
     
        static  IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
       
        public static string[]  ValidDomains{
             get {
              var domains =configuration.GetSection("ValidDomains").Get<string[]>();
              return domains;
            }
        }
          public static string  ConnectionString{
             get {
              return configuration.GetConnectionString("DemoDbContext");
            }
        }
     
    }
}
