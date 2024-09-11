using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    /// <summary>
    /// Employee respository.
    /// </summary>
    /// <seealso cref="IEmployeeRepository" />
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRespository"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="employeeContext">The employee context.</param>
        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        /// <summary>
        /// Adds an Employee entry.
        /// </summary>
        /// <param name="employee">The Employee.</param>
        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        /// <summary>
        /// Gets an Employee entry by the Id.
        /// </summary>
        /// <param name="id">The Id.</param>
        public Employee GetById(string id)
        {
            List<Employee> unnecessaryEmployeeList = _employeeContext.Employees.ToList();
            return unnecessaryEmployeeList.SingleOrDefault(e => e.EmployeeId == id);
        }
        
        /// <summary>
        /// Saves the changes to the employee.
        /// </summary>
        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes an employee entry.
        /// </summary>
        /// <param name="employee">The employee.</param>
        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
