using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TTS_boilerplate.Configuration;
using TTS_boilerplate.Web;

namespace TTS_boilerplate.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class TTS_boilerplateDbContextFactory : IDesignTimeDbContextFactory<TTS_boilerplateDbContext>
    {
        public TTS_boilerplateDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TTS_boilerplateDbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            TTS_boilerplateDbContextConfigurer.Configure(builder, configuration.GetConnectionString(TTS_boilerplateConsts.ConnectionStringName));

            return new TTS_boilerplateDbContext(builder.Options);
        }
    }
}
