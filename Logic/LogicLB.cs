using LoadBalancer.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoadBalancer.Logic
{
    public class LogicLB
    {
        public Machine getMachine()
        {
            IOrderedEnumerable<Machine> machines = Program.machines.OrderByDescending(x => x.Property).ThenBy(x => decimal.Divide(x.ReqUsed, x.MaxReq) * 100);

            return machines.First();
        }

        public OutputDto requestSunset(InputDto input)
        {
            Machine machine = getMachine();
            RestClient client = new RestClient(new Uri(machine.Url, "sun/sunset"));
            var request = new RestRequest();
            request.AddJsonBody(input);
            var response = client.GetAsync<OutputDto>(request);
            response.Wait();

            return response.Result;
        }

        public OutputDto requestSunrise(InputDto input)
        {
            Machine machine = getMachine();
            RestClient client = new RestClient(new Uri(machine.Url, "sun/sunrise"));
            var request = new RestRequest();
            request.AddJsonBody(input);
            var response = client.GetAsync<OutputDto>(request);
            response.Wait();

            return response.Result;
        }

        public bool existsMachine(Machine machine)
        {
            if (Program.machines.Exists(x => x.Url == machine.Url))
                return true;

            return false;
        }

        public void addMachine(Machine machine)
        {
            Program.machines.Add(machine);
        }

        public void removeMachine(Machine machine)
        {
            Program.machines.Remove(machine);
        }
    
        public List<Machine> getAllMachines()
        {
            return Program.machines;
        }

    }
}
