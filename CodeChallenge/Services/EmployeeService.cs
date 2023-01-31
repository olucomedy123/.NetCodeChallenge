using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using System.Collections;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }
        
        /*This method creates a new reporting structure. Then it will get the employee that you want to get a direct
         * report from first and add it to an arraylist. When a user has a directreport, the employee will be added
         * to the array list. Once that is done, the element will be removed from the arraylist. The loop will continue
         * until there is no more elements in the list.
        */
        public ReportingStructure GetReportById(String id)
        {
            ReportingStructure reportingStructure = new ReportingStructure();           
            if (!String.IsNullOrEmpty(id))
            {
                reportingStructure.Employee = _employeeRepository.GetById(id);
                ArrayList arrayList= new ArrayList();
                arrayList.Add(reportingStructure.Employee);
                int count = 0;
                while(arrayList.Count > 0)
                {
                    Employee tempEmployee = arrayList[0] as Employee;               
                    if (tempEmployee.DirectReports != null)
                    {
                        for(int i = 0; i< tempEmployee.DirectReports.Count; i++)
                        {
                            arrayList.Add(tempEmployee.DirectReports[i]);
                            count++;
                        }
                    }

                    arrayList.RemoveAt(0);
                }             
                reportingStructure.numberOfReports = count;
                return reportingStructure;
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }
    }
}
