using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class Compensation
    {   
        [Key] public string CompensationId { get; set; }
        [ForeignKey("EmployeeId")] public string EmployeeId { get; set; }
         public Employee Employee { get; set; }

         public String Salary { get; set; }

         public String EffectiveDate { get; set; }
    }
}
