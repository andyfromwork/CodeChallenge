using System;

namespace CodeChallenge.Models
{
    /// <summary>
    /// Compensation Entry.
    /// </summary>
    public class Compensation
    {
        // Per the instructions I think we were looking to use the "Employee" itself rather than just a reference number/primary key.
        // In a real world example I would try to store the bulk of the information once and then reference it as needed, like adding a GUID to match on.    
        public string Id { get; set; }

        public Employee Employee { get; set; }

        public decimal Salary { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}