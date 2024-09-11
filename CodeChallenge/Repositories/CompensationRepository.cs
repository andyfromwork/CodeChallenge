using System;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Repositories
{
    /// <summary>
    /// Compensation repository.
    /// </summary>
    /// <seealso cref="ICompensationRepository" />
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompensationRepository"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="compensationContext">The compensation context.</param>
        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new compensation entry.
        /// </summary>
        /// <param name="compensation">The compensation.</param>
        public Compensation Add(Compensation compensation)
        {
            compensation.Id = Guid.NewGuid().ToString();
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }

        /// <summary>
        /// Gets a compensation entry by id.
        /// </summary>
        /// <param name="id">The id.</param>
        public Compensation GetById(string id)
        {
            // not sure why I needed to include the employee value, but it wasn't displaying without this. 
            return _compensationContext.Compensations.Include(c => c.Employee).SingleOrDefault(e => e.Employee.EmployeeId == id);
        }

        /// <summary>
        /// Saves the changes to the compensation entry.
        /// </summary>
        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}