using HR.Models;
using HR.DTOs.Employees;
using Microsoft.AspNetCore.Mvc;

namespace HR.Controllers
{
    // Data Annotation - Extra information given to a class, method or property
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public static List<Employee> employeesList =
        [
            new () { Id = 1, Name = "Bara' Yaish", Age = 23, Position = "Developer", IsActive = true, StartDate = new DateTime(2025, 1, 1) },
            new () { Id = 2, Name = "Arab Yaish", Age = 23, Position = "Manager", IsActive = true, StartDate = new DateTime(2015, 10, 24) },
            new () { Id = 3, Name = "Raab Yaish", Age = 23, Position = "HR", IsActive = false, StartDate = new DateTime(2020, 5, 27) },
        ];

        [HttpGet("GetAll")]
        public IActionResult GetAll(string? position)
        {
            var employees = from employee in employeesList
                            where (position == null || employee.Position == position)
                            orderby employee.Age
                            select new EmployeeDto
                            {
                                Id = employee.Id,
                                Name = employee.Name,
                                Position = employee.Position,
                                Age = employee.Age,
                                IsActive = employee.IsActive,
                                StartDate = employee.StartDate
                            };

            return Ok(employees);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(long id)
        {
            var employee = employeesList
                .Select(employee => new EmployeeDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Position = employee.Position,
                    Age = employee.Age,
                    IsActive = employee.IsActive,
                    StartDate = employee.StartDate
                })
                .FirstOrDefault(employee => employee.Id == id);

            return Ok(employee);
        }

        [HttpPost("Add")]
        public IActionResult Add(SaveEmployeeDto newEmployee)
        {
            employeesList.Add(new ()
            {
                Id = (employeesList.LastOrDefault()?.Id ?? 0) + 1,
                Name = newEmployee.Name,
                Position = newEmployee.Position,
                Age = newEmployee.Age,
                IsActive = newEmployee.IsActive,
                StartDate = newEmployee.StartDate,
                EndDate = newEmployee.EndDate
            });

            return Created();
        }

        [HttpPut("Update")]
        public IActionResult Update(SaveEmployeeDto updateEmployee)
        {
            var targetEmployee = employeesList
                .FirstOrDefault(employee => employee.Id == updateEmployee.Id);

            if (targetEmployee is null)
            {
                return BadRequest($"Employee ({updateEmployee.Id}) Not Found");
            }

            targetEmployee.Name = updateEmployee.Name;
            targetEmployee.Position = updateEmployee.Position;
            targetEmployee.Age = updateEmployee.Age;
            targetEmployee.IsActive = updateEmployee.IsActive;
            targetEmployee.StartDate = updateEmployee.StartDate;
            targetEmployee.EndDate = updateEmployee.EndDate;

            return NoContent();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(long id)
        {
            var targetEmployee = employeesList
                .FirstOrDefault(employee => employee.Id == id);

            if (targetEmployee is null)
            {
                return BadRequest($"Employee ({id}) Not Found");
            }

            employeesList.Remove(targetEmployee);
            return Ok();
        }
    }
}
