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

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            var empid = Guid.NewGuid().ToString();
            compensation.EmployeeId = empid;
            compensation.Employee.EmployeeId = empid;
            _employeeContext.Compensations.Add(compensation);
            var compdb = _employeeContext.Compensations;
            return compensation;
        }

        public Compensation GetById(string id)
        {
            var compensation = new Compensation();
            var tpemmp = _employeeContext.Compensations.ToArray();
            foreach (var t in tpemmp)
            {
                if (t.EmployeeId == id)
                {
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
