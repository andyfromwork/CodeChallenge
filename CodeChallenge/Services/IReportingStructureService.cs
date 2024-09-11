using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    /// <summary>
    /// Reporting structure service.
    /// </summary>
    public interface IReportingStructureService
    {
        ReportingStructure GetById(String id);
    }
}