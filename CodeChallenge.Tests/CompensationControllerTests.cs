using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        // We could stand up a private Compensation here and use the same details for each of the tests.

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensationEntry_Returns_Created()
        {
            // Arrange
            var compensationEntry = new Compensation()
            {
                Employee = new Employee
                {
                    EmployeeId = Guid.NewGuid().ToString(),
                    FirstName = "Cody",
                    LastName = "Rhodes",
                    Department = "WWE",
                    Position = "Undisputed Champion",
                    DirectReports = new List<Employee>()
                },
                Salary = 1000000,
                EffectiveDate = DateTime.Today
            };

            var requestContent = new JsonSerialization().ToJson(compensationEntry);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensationEntry = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensationEntry.Id);
            Assert.AreEqual(compensationEntry.Salary, newCompensationEntry.Salary);
            Assert.AreEqual(compensationEntry.EffectiveDate, newCompensationEntry.EffectiveDate);
            Assert.AreEqual(compensationEntry.Employee.FirstName, newCompensationEntry.Employee.FirstName);
            Assert.AreEqual(compensationEntry.Employee.LastName, newCompensationEntry.Employee.LastName);
            Assert.AreEqual(compensationEntry.Employee.Department, newCompensationEntry.Employee.Department);
            Assert.AreEqual(compensationEntry.Employee.Position, newCompensationEntry.Employee.Position);
        }

        [TestMethod]
        public void GetCompensationEntry_Returns_Ok()
        {
            // This will be a two step process, as the InMemoryDB values to check will need to be created first.
            // Because of this, we may not need a standalone 'create' unit test, as that would always be included here.

            var compensationEntry = new Compensation()
            {
                Employee = new Employee
                {
                    EmployeeId = Guid.NewGuid().ToString(),
                    FirstName = "LA",
                    LastName = "Knight",
                    Department = "WWE",
                    Position = "United States Champion",
                    DirectReports = new List<Employee>()
                },
                Salary = 1200000,
                EffectiveDate = DateTime.Today
            };

            var requestContent = new JsonSerialization().ToJson(compensationEntry);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{compensationEntry.Employee.EmployeeId}");
            var response = getRequestTask.Result;

            // Assert
            var CompensationResponse = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(CompensationResponse.Id);
            Assert.AreEqual(compensationEntry.Salary, CompensationResponse.Salary);
            Assert.AreEqual(compensationEntry.EffectiveDate, CompensationResponse.EffectiveDate);
            Assert.AreEqual(compensationEntry.Employee.FirstName, CompensationResponse.Employee.FirstName);
            Assert.AreEqual(compensationEntry.Employee.LastName, CompensationResponse.Employee.LastName);
            Assert.AreEqual(compensationEntry.Employee.Department, CompensationResponse.Employee.Department);
            Assert.AreEqual(compensationEntry.Employee.Position, CompensationResponse.Employee.Position);
        }


        [TestMethod]
        public void GetCompensationEntryFromEmptyDB_Returns_0()
        {
            // Arrange
            var compensationEntry = new Compensation()
            {
                Employee = new Employee
                {
                    EmployeeId = Guid.NewGuid().ToString(),
                    FirstName = "Liv",
                    LastName = "Morgan",
                    Department = "WWE",
                    Position = "Women's World Champion",
                    DirectReports = new List<Employee>()
                },
                Salary = 1250000,
                EffectiveDate = DateTime.Today
            };
            var requestContent = new JsonSerialization().ToJson(compensationEntry);

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{compensationEntry.Employee.EmployeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}