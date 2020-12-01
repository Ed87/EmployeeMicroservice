using EmployeeAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeAPI.Repository
{
    public class EmployeeRepository : IEmployeeService
    {
        private readonly EmployeeContext _dbContext;

        public EmployeeRepository(EmployeeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateEmployee(Employee employee)
        {
            _dbContext.Add(employee);
            Save();
        }

        public void DeleteEmployee(int employeeId)
        {
            var product = _dbContext.Employees.Find(employeeId);
            _dbContext.Employees.Remove(product);
            Save();
        }

        public void UpdateEmployee(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Modified;
            Save();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _dbContext.Employees.Find(employeeId);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _dbContext.Employees.ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
