﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Sex { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string CreatedBy { get; set; }

        public int EmploymentTypeId { get; set; }

    }
}