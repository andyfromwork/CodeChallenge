using System.Collections.Generic;
using System.Linq;
using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Repositories
{
    /// <summary>
    /// Reporting structure repository.
    /// </summary>
    /// <seealso cref="IReportingStructureRepository" />
    public class ReportingStructureRepository : IReportingStructureRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IReportingStructureRepository> _logger;
        int employeeCount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingStructureRepository"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="employeeContext">The employee context.</param>
        public ReportingStructureRepository(ILogger<IReportingStructureRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        /// <summary>
        /// Gets the reporting structure by id.
        /// </summary>
        /// <param name="id">The id.</param>
        public ReportingStructure GetById(string id)
        {
            // I had an issue for a while on this. 
            // The function worked but if I ran the code without any breakpoint "Direct Reports" was always null.
            // I experienced this even on the base GET functions that were provided with the solution.
            // After some research, .net6 and InMemoryDB functions can get wonky.
            // Ideally we wouldn't need to convert this value to a list, but I was able to get results every time by accessing it this way.

            List<Employee> unnecessaryEmployeeList = _employeeContext.Employees.ToList();

            // Defaulting the number of Reports to 0 so that if there aren't any, we can just return RS without needing to Traverse through the children.

            ReportingStructure rs = new ReportingStructure
            {
                Employee = unnecessaryEmployeeList.SingleOrDefault(e => e.EmployeeId == id),
                NumberOfReports = 0
            };

            // Unit test of passing in text as a string triggered an error, we can't assume that it will always be a GUID, and that it will always return an employee.

            if (rs?.Employee?.DirectReports != null)
            {
                Traverse(rs.Employee);
                rs.NumberOfReports = employeeCount;
            }

            return rs;
        }

        /// <summary>
        /// Function to make sure we get all children/grand-children of the Employee.
        /// </summary>
        /// <param name="e">The employee.</param>
        public void Traverse(Employee e)
        {
            if (e?.DirectReports == null)
            {
                return;
            }

            foreach (Employee dr in e.DirectReports)
            {
                employeeCount++;
                Traverse(dr);
            }
        }
    }
}