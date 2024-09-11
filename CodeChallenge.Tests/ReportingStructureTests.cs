using System;
using System.Net.Http;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

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
        public void GetReportingStructureWithMultipleRecursions_Returns_ReportingStructure()
        {
            // Arrange
            var id = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedNumberOfReports = 4;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/ReportingStructure/{id}");
            var returnetReportingStructure = getRequestTask.Result.DeserializeContent<ReportingStructure>();

            // Assert
            Assert.AreEqual(expectedNumberOfReports, returnetReportingStructure.NumberOfReports);
        }
        

        [TestMethod]
        public void GetReportingStructureWithSingleRecursion_Returns_ReportingStructure()
        {
            // Arrange
            var id = "03aa1462-ffa9-4978-901b-7c001562cf6f";
            var expectedNumberOfReports = 2;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/ReportingStructure/{id}");
            var returnetReportingStructure = getRequestTask.Result.DeserializeContent<ReportingStructure>();

            // Assert
            Assert.AreEqual(expectedNumberOfReports, returnetReportingStructure.NumberOfReports);
        }
                

        [TestMethod]
        public void GetReportingStructureWithNoReports_Returns_ReportingStructure()
        {
            // Arrange
            var id = "c0c2293d-16bd-4603-8e08-638a9d18b22c";
            var expectedNumberOfReports = 0;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/ReportingStructure/{id}");
            var returnetReportingStructure = getRequestTask.Result.DeserializeContent<ReportingStructure>();

            // Assert
            Assert.AreEqual(expectedNumberOfReports, returnetReportingStructure.NumberOfReports);
        }
                     

        [TestMethod]
        public void GetReportingStructureWithNonExistent_Returns_ReportingStructure()
        {
            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/ReportingStructure/{Guid.NewGuid()}");
            var returnetReportingStructure = getRequestTask.Result.DeserializeContent<ReportingStructure>();

            // Assert
            Assert.AreEqual(null, returnetReportingStructure.Employee);
        }


        [TestMethod]
        public void GetReportingStructureWithNonGUIDPassed_Returns_Null_Employee()
        {
            // Arrange
            var id = "THISSHOULDNTWORK";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/ReportingStructure/{id}");
            var returnetReportingStructure = getRequestTask.Result.DeserializeContent<ReportingStructure>();

            // Assert
            Assert.AreEqual(null, returnetReportingStructure.Employee);
        }
    }
}
