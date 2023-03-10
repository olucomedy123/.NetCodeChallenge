using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/DirectReport")]
    public class DirectReportController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public DirectReportController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        //Get Restapi for getting reporting structure
        [HttpGet("{id}", Name = "GetReportingStructureById")]
        public IActionResult GetReportingStructureById(String id)
        {
            _logger.LogDebug($"Received Reports get request for '{id}'");

           var reports = _employeeService.GetReportById(id);

            if (reports.Employee == null)
               return NotFound();

            return Ok(reports);
        }

    }
}
