using System;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    // Originally I was going to add in the reporting structure on to the employee controller.
    // Thinking about how this might be expanded in the future, though, (if we ever wanted to use this heirarchy info for something meaningful)
    // I would rather have it separated than get tangled up with the employee details.

    [ApiController]
    [Route("api/reportingstructure")]

    /// <summary>
    /// Reporting structure controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingStructureController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="reportingStructureService">The reporting structure service.</param>
        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }
      
        /// <summary>
        /// Gets reporting structure by id.
        /// </summary>
        /// <param name="id">The id.</param>
        [HttpGet("{id}", Name = "getReportingStructureById")]
        public IActionResult GetReportingStructureById(String id)
        {
            var reportingStructure = _reportingStructureService.GetById(id);
            return Ok(reportingStructure);
        }
    }
}