using HR.Models;
using Microsoft.AspNetCore.Mvc;

namespace HR.Controllers
{
    // Data Annotation - Extra information given to a class, method or property
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        List<Employee> employeesList =
        [
            new () { Name = "Bara' Yaish", Age = 23, Position = "Developer" },
            new () { Name = "Omar Yaish", Age = 23, Position = "Developer" },
            new () { Name = "Someone", Age = 25, Position = "HR" },
            new () { Name = "Someone else", Age = 23, Position = "Manager" },
        ];

        [HttpGet("GetAll")]
        public IActionResult GetEmployee([FromQuery] string position)
        {
            var employees = from employee in employeesList
                            where employee.Position == position
                            orderby employee.Age
                            select new 
                            { 
                                employee.Name,
                                employee.Position,
                            };

            return Ok(employees);
        }
    }
}
