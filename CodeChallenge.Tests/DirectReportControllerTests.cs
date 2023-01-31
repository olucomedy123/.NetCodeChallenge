
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class DirectReportControllerTests
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

        //This is to test when a user has multiple direct reports and it returns the correct number
        [TestMethod]
        public void GetDirectReportByEmployeeID()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";
            var numberofreports = 4;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/DirectReport/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var employee = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(numberofreports, employee.numberOfReports);
            Assert.AreEqual(expectedLastName, employee.Employee.LastName);
            Assert.AreEqual(expectedFirstName, employee.Employee.FirstName);

        }

        //This is to test if the employeeid does not return an employee and that the code returns not found
        [TestMethod]
        public void GetNotFoundDirectReportByEmployeeID()
        {
            // Arrange
            var employeeId = "123";


            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/DirectReport/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }

        //This is to test if the employeeid returns an employee but that employee has 0 directreports.
        [TestMethod]
        public void GetZeroDirectReportByEmployeeID()
        {
            // Arrange
            var employeeId = "62c1084e-6e34-4630-93fd-9153afb65309";
            var expectedFirstName = "Pete";
            var expectedLastName =  "Best";
            var numberofreports = 0;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/DirectReport/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var employee = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(numberofreports, employee.numberOfReports);
            Assert.AreEqual(expectedLastName, employee.Employee.LastName);
            Assert.AreEqual(expectedFirstName, employee.Employee.FirstName);

        }

    }
}
