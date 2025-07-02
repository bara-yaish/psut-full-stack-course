using HR.DTOs.Departments;
using HR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace HR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private static List<Department> _departments = new()
        {
            new Department { Id = 1, Name = "IT", Description = "Provides technical assistance to staff and clients", FloorNumber = 4 },
            new Department { Id = 2, Name = "Development", Description = "Builds and maintains software products and applications", FloorNumber = 3 },
            new Department { Id = 3, Name = "Sales & Marketing", Description = "Promotes products, acquires customers, and drives revenue", FloorNumber = 1 }
        };

        [HttpGet("GetAll")]
        public IActionResult GetAllDepartments(string? name)
        {
            try
            {
                var allDepartments = _departments
                    .Where(department => (name == null || department.Name.Equals(name)))
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
        public IActionResult GetDepartmentById (long id)
        {
            try
            {
                var department = _departments
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
        public IActionResult AddDepartment(SaveDepartmentDto newDepartment)
        {
            try
            {
                _departments.Add(new Department()
                {
                    Id = (_departments.LastOrDefault()?.Id ?? 0) + 1,
                    Name = newDepartment.Name,
                    Description = newDepartment.Description,
                    FloorNumber = newDepartment.FloorNumber
                });

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult UpdateDepartment(SaveDepartmentDto updateDepartment)
        {
            try
            {
                var department = _departments
                    .FirstOrDefault(department => department.Id == updateDepartment.Id);

                if (department == null) return NotFound($"Department with ID ({updateDepartment.Id}) does not exist");

                department.Name = updateDepartment.Name;
                department.Description = updateDepartment.Description;
                department.FloorNumber = updateDepartment.FloorNumber;

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteDepartment(long id)
        {
            try
            {
                var department = _departments
                    .FirstOrDefault(department => department.Id == id);

                if (department == null) return NotFound($"Department with ID ({id}) does not exist");

                _departments.Remove(department);

                return Ok($"Department with ID ({id}) is deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
