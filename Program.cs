using LoadBalancer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LoadBalancer
{
    public class Program
    {
        public static string name = "Load Balancer";
        public static string fileConfig = @"InitialConfig.json";

        public static List<Machine> machines;

        public static void Main(string[] args)
        {
            chargeInitialConfig(fileConfig);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static bool chargeInitialConfig(string file)
        {
            try
            {
                using (StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();
                    machines = JsonConvert.DeserializeObject<List<Machine>>(json);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                machines = new List<Machine>();
                return false;
            }
        }

    }
}
