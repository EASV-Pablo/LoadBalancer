using LoadBalancer.Logic;
using LoadBalancer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoadBalancer
{
    public class Program
    {
        public static string name = "Load Balancer";
        public static string fileConfig = @"initialConfig.json";
        public static List<Machine> machines;

        public static void Main(string[] args)
        {
            new LogicLB().chargeInitialConfig(fileConfig);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
