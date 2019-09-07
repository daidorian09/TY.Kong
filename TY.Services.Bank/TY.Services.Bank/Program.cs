using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TY.Services.Bank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls(urls: "http://+:5070")
                .UseStartup<Startup>();
    }
}
