using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Sex { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string CreatedBy { get; set; }

        [Required]
        public int EmploymentTypeId { get; set; }

    }
}
