using HR.DTOs.Employees;
using Microsoft.AspNetCore.Mvc;

namespace HR.Controllers
{
    // Data Annotation - Extra information given to a class, method or property
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

        private HrDbContext _dbContext;

        // Dependency Injection
        public EmployeesController(HrDbContext dbContext) { _dbContext = dbContext; }

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] FilterEmployeeDto filters)
        {
            var employees = from employee in _dbContext.Employees
                            from department in _dbContext.Departments.Where(x => x.Id == employee.DepartmentId).DefaultIfEmpty()
                            from manager in _dbContext.Employees.Where(x => x.Id == employee.ManagerId).DefaultIfEmpty()
                            where 
                                (filters.Name == null || employee.Name.ToLower().Contains(filters.Name.ToLower())) && 
                                (filters.Position == null || employee.Position.ToLower().Contains(filters.Position.ToLower())) && 
                                (filters.IsActive || employee.IsActive == filters.IsActive)
                            orderby 
                                employee.Id
                            select 
                                new EmployeeDto
                                {
                                    Id = employee.Id,
                                    Name = employee.Name,
                                    Position = employee.Position,
                                    Age = employee.Age,
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
                .Select(employee => new EmployeeDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Position = employee.Position,
                    Age = employee.Age,
                    IsActive = employee.IsActive,
                    StartDate = employee.StartDate,
                    Phone = employee.Phone,
                    ManagerId = employee.ManagerId,
                    DepartmentId = employee.DepartmentId,
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
            _dbContext.Employees.Add(new ()
            {
                Name = newEmployee.Name,
                Age = newEmployee.Age,
                Phone = newEmployee.Phone,
                Position = newEmployee.Position,
                IsActive = newEmployee.IsActive,
                StartDate = newEmployee.StartDate,
                EndDate = newEmployee.EndDate,
                DepartmentId = newEmployee.DepartmentId,
                ManagerId = newEmployee.ManagerId,
            });
            _dbContext.SaveChanges(); //It won't save the added changes unless this method is called

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
            targetEmployee.Age = updateEmployee.Age;
            targetEmployee.Phone = updateEmployee.Phone;
            targetEmployee.Position = updateEmployee.Position;
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
