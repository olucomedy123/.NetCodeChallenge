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
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext, IEmployeeRepository employeeRepository)
        {
            _employeeContext = employeeContext;
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        /*This will add a compensation to the compensation dbset. If the compensation passed in has a employee id but not an
         * employee, then the code will call GetById to add the employee to the compensation. This is assuming the user
         * knows what employee id to get similar to updating employee. Else the code will just create a new employee
         * and add it to the dbset
         */
        public Compensation Add(Compensation compensation)
        {
            if (compensation.EmployeeId != null && compensation.Employee == null)
            {
                compensation.Employee = _employeeRepository.GetById(compensation.EmployeeId);
            }
            else
            {
                var empid = Guid.NewGuid().ToString();
                compensation.Employee.EmployeeId = empid;
                compensation.EmployeeId = empid;
                _employeeRepository.Add(compensation.Employee);
            }
            compensation.CompensationId = Guid.NewGuid().ToString();
            _employeeContext.Compensations.Add(compensation);
            var compdb = _employeeContext.Compensations;
            return compensation;
        }

        public Compensation GetById(string id)
        {
            var compensation = new Compensation();
            var test = _employeeContext.Compensations;
            var tpemmp = _employeeContext.Compensations.ToArray();
            foreach (var t in tpemmp)
            {
                if (t.EmployeeId == id)
                {
                    t.Employee = _employeeRepository.GetById(id);
                    return t;
                }
            }
            return null;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}
