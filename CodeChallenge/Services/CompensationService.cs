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
    /// Compensation service.
    /// </summary>
    /// <seealso cref="ICompensationService" />
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompensationService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="compensationRepository">The compensation repository.</param>
        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="compensation">The compensation.</param>
        public Compensation Create(Compensation compensation)
        {
            _compensationRepository.Add(compensation);
            _compensationRepository.SaveAsync().Wait();

            return compensation;
        }

        /// <summary>
        /// Gets the compensation entry by id.
        /// </summary>
        /// <param name="id">The id.</param>
        public Compensation GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }

            return null;
        }
    }
}