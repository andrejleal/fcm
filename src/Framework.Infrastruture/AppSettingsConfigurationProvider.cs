using Microsoft.Extensions.Configuration;
using System.IO;

namespace Framework.Infrastruture
{
    public class AppSettingsConfigurationProvider : IConfigurationProvider
    {
        protected readonly IConfigurationRoot root;
        private int repositoryPaginationMaxPageSize = -1;

        public AppSettingsConfigurationProvider()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            System.Console.WriteLine("CH: "+ Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false);
            this.root = builder.Build();
        }

        public AppSettingsConfigurationProvider(IConfigurationRoot root)
        {
            this.root = root;
        }
        
        public string GetConnectionString(string name)
        {
            return this.root.GetConnectionString(name);
        }
    }
}
