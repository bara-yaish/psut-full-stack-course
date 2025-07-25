using HR.DTOs.Employees;
using HR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR.Controllers
{
    // Data Annotation - Extra information given to a class, method or property
    [Authorize] // To enable Jwt Authentication and Authorization we implemented
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // Simple Data Type : long, int, string.... | Query Parameter (By Default)
        // Complex Data Type : Model, Dto (object)  | Request Body (By Default)

        // Http Get : Can Not Use Body Request [FromBody], We Can Only Use Query Parameter [FromQuery]
        // Http Put/Post : Can Use Both Body Request [FromBody] And Query Parameter [FromQuery], But We Will Only Use [FromBody]
        // Http Delete : Can Use Both Body Request [FromBody] And Query Parameter [FromQuery], But We Will Only Use [FromQuery]

        // Can't Use Multiple Parameters Of Type [FromBody]
        // Can Use Multiple Parameters Of Type [FromQuery]

        private readonly HrDbContext _dbContext;

        // Dependency Injection
        public EmployeesController(HrDbContext dbContext) { _dbContext = dbContext; }

        [Authorize(Roles = "HR, Admin")]
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] FilterEmployeeDto filters)
        {
            var employees = from employee in _dbContext.Employees.AsNoTracking()
                            from department in _dbContext.Departments.AsNoTracking().Where(x => x.Id == employee.DepartmentId).DefaultIfEmpty() // Where == INNER JOIN | DefaultIfEmpty == LEFT JOIN
                            from manager in _dbContext.Employees.AsNoTracking().Where(x => x.Id == employee.ManagerId).DefaultIfEmpty()
                            from lookup in _dbContext.Lookups.AsNoTracking().Where(x => x.Id == employee.PositionId).DefaultIfEmpty()
                            where
                                (filters.PositionId == null || employee.PositionId == filters.PositionId) &&
                                (string.IsNullOrWhiteSpace(filters.Name) || employee.Name.ToLower().Contains(filters.Name.ToLower())) &&
                                (filters.PositionId == null || employee.PositionId == filters.PositionId) &&
                                (filters.IsActive == null || employee.IsActive == filters.IsActive)
                            orderby
                                employee.Id
                            select
                                new EmployeeDto
                                {
                                    Id = employee.Id,
                                    Name = employee.Name,
                                    PositionId = employee.PositionId,
                                    PositionName = lookup.Name,
                                    BirthDate = employee.BirthDate,
                                    IsActive = employee.IsActive,
                                    StartDate = employee.StartDate,
                                    Phone = employee.Phone,
                                    ManagerId = employee.ManagerId,
                                    ManagerName = manager.Name,
                                    DepartmentId = employee.DepartmentId,
                                    DepartmentName = department.Name
                                };

            return Ok(employees);
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery] long id)
        {
            var employee = _dbContext.Employees
                .AsNoTracking()
                .Select(employee => new EmployeeDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    BirthDate = employee.BirthDate,
                    PositionId = employee.PositionId,
                    PositionName = employee.Lookup.Name,
                    IsActive = employee.IsActive,
                    StartDate = employee.StartDate,
                    Phone = employee.Phone,
                    ManagerId = employee.ManagerId,
                    ManagerName = employee.Manager.Name,
                    DepartmentId = employee.DepartmentId,
                    DepartmentName = employee.Department.Name
                })
                .FirstOrDefault(employee => employee.Id == id);

            if (employee is null)
            {
                return BadRequest($"Employee {id} Not Found");
            }

            return Ok(employee);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] SaveEmployeeDto newEmployee)
        {
            var newUser = new User
            {
                UserName = $"{newEmployee.Name}_HR",
                HashedPassword = BCrypt.Net.BCrypt.HashPassword($"{newEmployee.Name}@123"),
                IsAdmin = false
            };
            _dbContext.Add(newUser);

            _dbContext.Employees.Add(new ()
            {
                Name = newEmployee.Name,
                BirthDate = newEmployee.BirthDate,
                PositionId = newEmployee.PositionId,
                IsActive = newEmployee.IsActive,
                StartDate = newEmployee.StartDate,
                EndDate = newEmployee.EndDate,
                DepartmentId = newEmployee.DepartmentId,
                ManagerId = newEmployee.ManagerId,
                User = newUser // EF will automatically reference the UserId in the new User record above
            });
            _dbContext.SaveChanges(); // It won't save the added changes unless this method is called

            return Created();
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] SaveEmployeeDto updateEmployee)
        {
            var targetEmployee = _dbContext.Employees
                .FirstOrDefault(employee => employee.Id == updateEmployee.Id);

            if (targetEmployee is null)
            {
                return BadRequest($"Employee {updateEmployee.Id} Not Found");
            }

            targetEmployee.Name = updateEmployee.Name;
            targetEmployee.BirthDate = updateEmployee.BirthDate;
            targetEmployee.PositionId = updateEmployee.PositionId;
            targetEmployee.IsActive = updateEmployee.IsActive;
            targetEmployee.StartDate = updateEmployee.StartDate;
            targetEmployee.EndDate = updateEmployee.EndDate;
            targetEmployee.DepartmentId = updateEmployee.DepartmentId;
            targetEmployee.ManagerId = updateEmployee.ManagerId;

            _dbContext.Employees.Update(targetEmployee);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] long id)
        {
            var targetEmployee = _dbContext.Employees
                .FirstOrDefault(employee => employee.Id == id);

            if (targetEmployee is null)
            {
                return BadRequest($"Employee {id} Not Found");
            }

            _dbContext.Employees.Remove(targetEmployee);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
