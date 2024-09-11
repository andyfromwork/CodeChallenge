using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    /// <summary>
    /// Employee service.
    /// </summary>
    public interface IEmployeeService
    {
        // I considered adding the reportingstructure GetById here, but would prefer to have separate functionality in its own interface as it could grow over time.
        Employee GetById(String id);
        Employee Create(Employee employee);
        Employee Replace(Employee originalEmployee, Employee newEmployee);
    }
}
