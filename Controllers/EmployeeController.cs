using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using EmployeeAPI.Model;
using EmployeeAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Action to get all employees
        /// </summary>       
        /// <returns>Returns all employees</returns>
        /// <response code="200">Returned if all employees are retrieved</response>
        /// <response code="400">Returned if the model couldn't be parsed or the employees couldn't be found</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet]
        public IActionResult Get()
        {
            var employees = _employeeService.GetEmployees();
            return new OkObjectResult(employees);
        }

        /// <summary>
        /// Action to get an single employee
        /// </summary>
        /// <param name="id">Model to get a single employee</param>
        /// <returns>Returns the updated employee</returns>
        /// <response code="200">Returned if the employee was retrieved</response>
        /// <response code="400">Returned if the model couldn't be parsed or the employee couldn't be found</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            return new OkObjectResult(employee);
        }

       
        /// <summary>  
        /// Action to create a new employee in the database.  
        /// </summary>  
        /// <param name="employee">Model to create a new employee</param>  
        /// <returns>Returns the created customer</returns>  
        /// <response code="200">Returned if the employee was created</response>  
        /// <response code="400">Returned if the model couldn&#8217;t be parsed or the employee couldn&#8217;t be saved</response>  
        /// <response code="422">Returned when the validation failed</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            using (var scope = new TransactionScope())
            {
                _employeeService.CreateEmployee(employee);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = employee.EmployeeId }, employee);
            }
        }


        /// <summary>
        /// Action to update an existing employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee">Model to update an existing employee</param>
        /// <returns>Returns the updated employee</returns>
        /// <response code="200">Returned if the employee was updated</response>
        /// <response code="400">Returned if the model couldn't be parsed or the customer couldn't be found</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee != null)
            {
                using (var scope = new TransactionScope())
                {
                    _employeeService.UpdateEmployee(employee);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _employeeService.DeleteEmployee(id);
            return new OkResult();
        }
    }
}
