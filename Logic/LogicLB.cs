using LoadBalancer.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadBalancer.Logic
{
    public class LogicLB
    {
        public Machine getMachine()
        {
            Machine machine;
            lock (Program.machines)
            {
                machine = Program.machines.OrderByDescending(x => x.Property)
                                                     .ThenBy(x => decimal.Divide(x.ReqUsed, x.MaxReq) * 100)
                                                     .First();
                updateMachineState(machine, true);
            }       

            return machine;
        }

        public OutputDto requestSunset(InputDto input)
        {
            return doRequest(input, getMachine(), "sun/sunset").Result;
        }

        public OutputDto requestSunrise(InputDto input)
        {
            return doRequest(input, getMachine(), "sun/sunrise").Result;
        }

        private Task<OutputDto> doRequest(InputDto input, Machine machine, string urlEndpoint)
        {            

            RestClient client = new RestClient(new Uri(machine.Url, urlEndpoint));
            var request = new RestRequest();
            request.AddJsonBody(input);
            var response = client.PostAsync<OutputDto>(request);
            response.Wait();

            //lock (Program.machines)
            //{
            //    updateMachineState(machine, false);
            //}

            updateMachineState(machine, false);

            return response;
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

        private void updateMachineState(Machine machine, bool inUse)
        {
            if (inUse) {
                Program.machines.Find(x => x == machine).ReqUsed++;
            }
            else
            {
                Program.machines.Find(x => x == machine).ReqUsed--;
            }
        }

    }
}
