using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ONS.SDK.Extensions.Builder;
using ONS.SDK.Test.Web.Process;

namespace ONS.SDK.Test.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var envport = System.Environment.GetEnvironmentVariable("PORT");
            
            if (args.Length > 0) {
                envport = args[0];
            }

            var webHost = CreateWebHostBuilder(args);
            
            if (!string.IsNullOrEmpty(envport)) {
                webHost.UseUrls("http://*:" + envport);
            }

            webHost
                .AddSDKBind<CenarioBusiness>()
                .RunSDK();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
