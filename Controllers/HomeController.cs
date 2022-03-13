using LoadBalancer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LoadBalancer.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sun/sunset")]
        public OutputDto GetSunset([FromBody] InputDto input)
        {
            return requestSunset(input);
        }

        [HttpGet("sun/sunrise")]
        public OutputDto GetSunrise([FromBody] InputDto input)
        {
            
            RedirectPreserveMethod(new Uri(Program.machines.ElementAt(0).Url, "sun/sunrise").ToString());
            return new OutputDto();
        }

        [HttpDelete("machines")]
        public IActionResult DeleteMachine([FromBody] Machine machine)
        {
            if (existsMachine(machine))
            {
                removeMachine(machine);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("machines")]
        public IActionResult AddMachine([FromBody] Machine machine)
        {
            if (!existsMachine(machine))
            {
                addMachine(machine);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("machines")]
        public List<Machine> GetMachines()
        {
            return Program.machines;
        }

        private bool existsMachine(Machine machine)
        {
            if(Program.machines.Exists(x => x.Url == machine.Url))
                return true;
            
            return false;
        }

        private void addMachine(Machine machine)
        {
            Program.machines.Add(machine);
        }

        private void removeMachine(Machine machine)
        {
            Program.machines.Remove(machine);
        }

        private int strategy()
        {
            int numberOfmachines = Program.machines.Count;
            return new Random().Next(numberOfmachines);
        }

        private OutputDto requestSunset(InputDto input)
        {
            Machine m = Program.machines.ElementAt(strategy());
            RestClient client = new RestClient(new Uri(m.Url, "sun/sunset"));
            var request = new RestRequest();
            request.AddJsonBody(input);
            var response = client.GetAsync<OutputDto>(request);
            response.Wait();

            return response.Result;
        }

        private OutputDto requestSunrise(InputDto input)
        {
            Machine m = Program.machines.ElementAt(strategy());
            RestClient client = new RestClient(new Uri(m.Url, "sun/sunrise"));
            var request = new RestRequest();
            request.AddJsonBody(input);
            var response = client.GetAsync<OutputDto>(request);
            response.Wait();

            return response.Result;
        }
    }
}
