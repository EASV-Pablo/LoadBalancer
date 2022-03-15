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
        public static List<Machine> machines;

        public static void Main(string[] args)
        {
            machines = new List<Machine>();
            machines.Add(new Machine(new Uri("http://192.168.0.13:5100/"), "Raspi 3", true, 10, 0));
            machines.Add(new Machine(new Uri("http://192.168.0.14:5100/"), "Raspi 4", true, 30, 0));
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
