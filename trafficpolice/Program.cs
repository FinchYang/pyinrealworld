using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace trafficpolice
{
    public class Program
    {
        private static int port = 8000;

        public static void Main(string[] args)
        {
            if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
                int.TryParse(args[0], out port);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>().UseUrls("http://*:" + port)
        .ConfigureLogging(builder => builder.AddFile())
                .Build();

    }
}