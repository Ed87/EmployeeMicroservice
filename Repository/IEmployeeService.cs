using EmployeeAPI.Model;
using System.Collections.Generic;

namespace EmployeeAPI.Repository
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();

        void CreateEmployee(Employee employee);

        void DeleteEmployee(int employeeId);

        void UpdateEmployee(Employee employee);

        Employee GetEmployeeById(int employeeId);

        void Save();

    }
}
