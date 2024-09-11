using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;

namespace CodeChallenge.Repositories
{
    /// <summary>
    /// Reporting structure repository interface.
    /// </summary>
    public interface IReportingStructureRepository
    {
        ReportingStructure GetById(String id);
    }
}