using LoadBalancer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;

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

        //[HttpGet("sun/sunset")]
        //public OutputDto GetSunset([FromBody] InputDto input)
        //{
        //    return requestSunset(input);
        //}

        //[HttpGet("sun/sunrise")]
        //public OutputDto GetSunrise([FromBody] InputDto input)
        //{
        //    return requestSunrise(input);
        //}

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
        public Machine GetMachines()
        {
            return Logic.LogicLB.getMachine();
        }

        private bool existsMachine(Machine machine)
        {
            if (Program.machines.Exists(x => x.Url == machine.Url))
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

        private OutputDto requestSunset(Machine machine, InputDto input)
        {
            RestClient client = new RestClient(new Uri(machine.Url, "sun/sunset"));
            var request = new RestRequest();
            request.AddJsonBody(input);
            var response = client.GetAsync<OutputDto>(request);
            response.Wait();

            return response.Result;
        }

        private OutputDto requestSunrise(Machine machine, InputDto input)
        {
            RestClient client = new RestClient(new Uri(machine.Url, "sun/sunrise"));
            var request = new RestRequest();
            request.AddJsonBody(input);
            var response = client.GetAsync<OutputDto>(request);
            response.Wait();

            return response.Result;
        }

    }
}
