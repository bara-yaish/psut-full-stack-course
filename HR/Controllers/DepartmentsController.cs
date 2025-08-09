using HR.DTOs.Departments;
using HR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly HrDbContext _dbContext;

        public DepartmentsController(HrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllDepartments([FromQuery] string? name)
        {
            try
            {
                var allDepartments = _dbContext.Departments
                    .Where(department => (name == null || department.Name.ToLower().Equals(name.ToLower())))
                    .Select(department => new DepartmentDto
                    {
                        Id = department.Id,
                        Name = department.Name,
                        Description = department.Description,
                        FloorNumber = department.FloorNumber
                    });

                return Ok(allDepartments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetDepartmentById ([FromQuery] long id)
        {
            try
            {
                var department = _dbContext.Departments
                    .Select(department => new DepartmentDto()
                    {
                        Id = department.Id,
                        Name = department.Name,
                        Description = department.Description,
                        FloorNumber = department.FloorNumber
                    })
                    .FirstOrDefault(department => department.Id == id);

                if (department == null) return NotFound($"Department with ID ({id}) does not exist");

                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add")]
        public IActionResult AddDepartment([FromBody] SaveDepartmentDto newDepartment)
        {
            try
            {
                _dbContext.Departments.Add(new Department()
                {
                    Name = newDepartment.Name,
                    Description = newDepartment.Description,
                    FloorNumber = newDepartment.FloorNumber
                });
                _dbContext.SaveChanges();

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult UpdateDepartment([FromBody] SaveDepartmentDto updateDepartment)
        {
            try
            {
                var department = _dbContext.Departments
                    .FirstOrDefault(department => department.Id == updateDepartment.Id);

                if (department == null) return NotFound($"Department with ID ({updateDepartment.Id}) does not exist");

                department.Name = updateDepartment.Name;
                department.Description = updateDepartment.Description;
                department.FloorNumber = updateDepartment.FloorNumber;

                _dbContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteDepartment([FromQuery] long id)
        {
            try
            {
                var department = _dbContext.Departments
                    .FirstOrDefault(department => department.Id == id);

                if (department == null) return NotFound($"Department with ID ({id}) does not exist");

                _dbContext.Departments.Remove(department);
                _dbContext.SaveChanges();

                return Ok($"Department with ID ({id}) is deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
