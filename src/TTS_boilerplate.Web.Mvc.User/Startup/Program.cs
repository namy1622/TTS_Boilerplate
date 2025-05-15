 using Abp.AspNetCore.Dependency;
using Abp.Dependency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TTS_boilerplate.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:44310", "http://0.0.0.0:44310");
                    //webBuilder.UseUrls("https://localhost:80", "https://0.0.0.0:80");
                })

                .UseCastleWindsor(IocManager.Instance.IocContainer);


    }
}
