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
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        /*This stores all of the Empployees into an array and then checks if an employee id in the array matches
         * the id that was passed in. I did this because the original code never returned an employess directreports.
         */
        public Employee GetById(string id)
        {
            

            var tpemmp = _employeeContext.Employees.ToArray();
            foreach (var t in tpemmp)
            {
                if (t.EmployeeId == id)
                {
                    return t;
                }
            }
            return null;
        }

        //This gets the direct reports of a specific user
        public List<Employee> GetReportById(string id)
        {           
            Employee employee = GetById(id);
            return employee.DirectReports;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
