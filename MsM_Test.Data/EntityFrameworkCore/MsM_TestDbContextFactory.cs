using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MsM_Test.Data.EntityFrameworkCore
{
    class MsM_TestDbContextFactory : IDesignTimeDbContextFactory<MsM_TestDbcontext>
    {
        public MsM_TestDbcontext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<MsM_TestDbcontext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MsM_Testdb"));

            return new MsM_TestDbcontext(optionsBuilder.Options);
        }
    }
}
