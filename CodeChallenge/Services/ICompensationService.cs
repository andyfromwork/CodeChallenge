using System;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    /// <summary>
    /// Compensation service.
    /// </summary>
    public interface ICompensationService
    {
        Compensation GetById(String id);
        Compensation Create(Compensation compensation);
    }
}