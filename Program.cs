using LoadBalancer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadBalancer
{
    public class Program
    {
        public static string name = "Load Balancer";
        public static List<Machine> machines;

        public static void Main(string[] args)
        {
            machines = new List<Machine>();
            machines.Add(new Machine(new Uri("http://192.168.0.13:5100/"), "Raspi 3", true));
            machines.Add(new Machine(new Uri("http://192.168.0.14:5100/"), "Raspi 4", true));
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
