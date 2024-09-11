using System;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    /// <summary>
    /// Compensation controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompensationController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="compensationService">The compensation service.</param>
        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        /// <summary>
        /// Creates employee.
        /// </summary>
        /// <param name="compensation">The compensation entry.</param>
        [HttpPost]
        public IActionResult CreateCompensationEntry([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.Employee.FirstName} {compensation.Employee.LastName}'");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationById", new { id = compensation.Employee.EmployeeId }, compensation);
        }

        /// <summary>
        /// Gets compensation by id.
        /// </summary>
        /// <param name="id">The id.</param>
        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _compensationService.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
    }
}