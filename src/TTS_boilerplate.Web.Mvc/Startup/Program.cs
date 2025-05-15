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
                    //webBuilder.UseUrls("http://localhost:44312", "http://0.0.0.0:44312");
                })
                .UseCastleWindsor(IocManager.Instance.IocContainer);
    }
}
