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
        /*this is the model for compensation. I wanted to add compensation id as the primary key because when I tried to
         * make the new dbset it always returned an error saying I needed one and I couldn't have employee id as the 
         * primary key. I also have emplyoee id as a foriegn key because when I originally stored employee into the dbset
         * it would never store or return the employee from the database. So I used employee id to link compensation table 
         * the employee table. This also allowed me to add new functionality to task 2(even if it wasnt needed) that I 
         * mentioned in the test cases.
         */
        [Key] public string CompensationId { get; set; }
        [ForeignKey("EmployeeId")] public string EmployeeId { get; set; }
         public Employee Employee { get; set; }

         public String Salary { get; set; }

         public String EffectiveDate { get; set; }
    }
}
