﻿using LoadBalancer.Logic;
using LoadBalancer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LoadBalancer.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LogicLB logicLB;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            logicLB = new LogicLB();
        }

        [HttpGet("sun/sunset")]
        public OutputDto GetSunset([FromBody] InputDto input)
        {
            return logicLB.requestSunset(input);
        }

        [HttpGet("sun/sunrise")]
        public OutputDto GetSunrise([FromBody] InputDto input)
        {
            return logicLB.requestSunrise(input);
        }


        [HttpDelete("machines")]
        public IActionResult DeleteMachine([FromBody] Machine machine)
        {
            if (logicLB.existsMachine(machine))
            {
                logicLB.removeMachine(machine);
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
            if (!logicLB.existsMachine(machine))
            {
                logicLB.addMachine(machine);
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
            return logicLB.getAllMachines();
        }

    }
}
