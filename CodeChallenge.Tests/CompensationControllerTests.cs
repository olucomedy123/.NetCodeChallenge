
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
    public class CompensationsControllerTests
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
        public void CreateNewEmployeeAndCompensation_Returns_Created()
        {
            // Arrange
            var employee = new Employee()
            {
                Department = "Complaints",
                FirstName = "Debbie",
                LastName = "Downer",
                Position = "Receiver",
            };
            //create compensation here
            var compisation = new Compensation()
            {
                Employee = employee,
                Salary = "2",
                EffectiveDate = "11/20/2023",
            };
            var requestContent = new JsonSerialization().ToJson(compisation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/CompensationService",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newEmployee = response.DeserializeContent<Compensation>();
             Assert.IsNotNull(newEmployee.EmployeeId);
            Assert.AreEqual(employee.FirstName, newEmployee.Employee.FirstName);
            Assert.AreEqual(employee.LastName, newEmployee.Employee.LastName);
            Assert.AreEqual(employee.Department, newEmployee.Employee.Department);
            Assert.AreEqual(employee.Position, newEmployee.Employee.Position);
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            //create compensation here
            var employee = new Employee()
            {
                EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                Department = "Engineering",
                FirstName = "Ringo",
                LastName = "Starr",
                Position = "Developer V",
            };
            var compisation = new Compensation()
            {
                EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                Salary = "2",
                EffectiveDate = "11/20/2023",
            };
            var requestContent = new JsonSerialization().ToJson(compisation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/CompensationService",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newEmployee = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newEmployee.EmployeeId);
            Assert.AreEqual(employee.FirstName, newEmployee.Employee.FirstName);
            Assert.AreEqual(employee.LastName, newEmployee.Employee.LastName);
            Assert.AreEqual(employee.Department, newEmployee.Employee.Department);
            Assert.AreEqual(employee.Position, newEmployee.Employee.Position);
        }


        [TestMethod]
        public void GetCompensationByEmployeeId_Returns_Ok()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                Department = "Engineering",
                FirstName = "Ringo",
                LastName = "Starr",
                Position = "Developer V",
            };
            var compisation = new Compensation()
            {
                EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                Salary = "2",
                EffectiveDate = "11/20/2023",
            };
            var requestContent = new JsonSerialization().ToJson(compisation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/CompensationService",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var postResponse = postRequestTask.Result;
            var getEmployee = postResponse.DeserializeContent<Compensation>();
            var employeeId = getEmployee.EmployeeId;
            var getRequestTask = _httpClient.GetAsync($"api/CompensationService/{employeeId}");
            var response = getRequestTask.Result;


            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.EmployeeId);
            Assert.AreEqual(employee.FirstName, newCompensation.Employee.FirstName);
            Assert.AreEqual(employee.LastName, newCompensation.Employee.LastName);
            Assert.AreEqual(employee.Department, newCompensation.Employee.Department);
            Assert.AreEqual(employee.Position, newCompensation.Employee.Position);
        }

        [TestMethod]
        public void GetEmployeeById_Returns_NotFound()
        {
            // Arrange
            var employee = new Employee()
            {
                Department = "Engineering",
                FirstName = "Ringo",
                LastName = "Starr",
                Position = "Developer V",
            };
            var compisation = new Compensation()
            {
                Salary = "2",
                Employee = employee,
                EffectiveDate = "11/20/2023",
            };
            var requestContent = new JsonSerialization().ToJson(compisation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/CompensationService",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var postResponse = postRequestTask.Result;
            var getEmployee = postResponse.DeserializeContent<Compensation>();
            var employeeId = "1";
            var getRequestTask = _httpClient.GetAsync($"api/CompensationService/{employeeId}");
            var response = getRequestTask.Result;


            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
} 
