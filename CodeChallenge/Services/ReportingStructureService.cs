using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    /// <summary>
    /// Reporting structure service.
    /// </summary>
    /// <seealso cref="IReportingStructureService" />
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IReportingStructureRepository _reportingStructureRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingStructureService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="reportingStructureRepository">The reporting structure repository.</param>
        public ReportingStructureService(ILogger<ReportingStructureService> logger, IReportingStructureRepository reportingStructureRepository)
        {
            _reportingStructureRepository = reportingStructureRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets the Reporting Structure by id.
        /// </summary>
        /// <param name="id">The id.</param>
        public ReportingStructure GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _reportingStructureRepository.GetById(id);
            }
            return null;
        }
    }
}