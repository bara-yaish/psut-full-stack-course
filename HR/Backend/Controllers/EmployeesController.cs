using HR.DTOs.Employees;
using HR.DTOs.Shared;
using HR.Enums;
using HR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace HR.Controllers
{
    // Data Annotation - Extra information given to a class, method or property
    //[Authorize] // To enable Jwt Authentication and Authorization we implemented
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
        private readonly IConfiguration _config;
        private IWebHostEnvironment _env;

        // Dependency Injection
        public EmployeesController(HrDbContext dbContext, IConfiguration config, IWebHostEnvironment env) 
        { 
            _dbContext = dbContext; 
            _config = config;
            _env = env;
        }

        //[Authorize(Roles = "HR, Admin")]
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
                                employee.Id descending
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
                                    DepartmentName = department.Name,
                                    ImagePath = !string.IsNullOrEmpty(employee.ImagePath) ? Path.Combine(_config["BaseUrl"], employee.ImagePath) : string.Empty
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

        [HttpGet("VacationsCount")]
        public IActionResult GetVacationCount()
        {
            /*
             * SELECT
	         *      T1.Id AS EmployeeId,
	         *      T1.[Name] AS EmployeeName,
	         *      COUNT(T2.Id) AS VacationsCount
             *  FROM
	         *      Employees T1
	         *      LEFT JOIN Vacations T2 ON T1.Id = T2.EmplopyeeId
             *  GROUP BY
	         *      T1.Id,
	         *      T1.[Name]
             */

            //return Ok(
            //    from employee in _dbContext.Employees.AsNoTracking()
            //    from vacation in _dbContext.Vacations.AsNoTracking().Where(x => x.EmployeeId == employee.Id).DefaultIfEmpty()
            //    group new { Employee = employee, Vacation = vacation } by new { employee.Id, Name = employee.Name } into vacationsCount
            //    select new EmployeeVacationsCountDto
            //    {
            //        EmployeeId = vacationsCount.Key.Id,
            //        EmployeeName = vacationsCount.Key.Name,
            //        VacationsCount = vacationsCount.ToList().Count(x => x.Vacation != null)
            //    });

            return Ok(_dbContext.Employees                                  // FROM Employees
                .AsNoTracking()
                // GroupJoin automatically groups by all
                // of the selected fields
                .GroupJoin(                                                 // LEFT JOIN
                    _dbContext.Vacations,                                   // Vacations ON
                    employee => employee.Id,                                // Employees.Id =
                    vacation => vacation.EmployeeId,                        // Vacations.EmployeeId
                    (employee, vacation) => new EmployeeVacationsCountDto   // SELECT
                    {
                        EmployeeId = employee.Id,                           // Employees.Id AS EmployeeId
                        EmployeeName = employee.Name,                       // Employees.Name AS EmployeeName
                        VacationsCount = vacation.Count()                   // COUNT(Vacations.Id)
                    })
                .ToList());
        }

        [HttpGet("Managers")]
        public IActionResult GetManagers([FromQuery] long? employeeId)
        {
            return Ok(_dbContext.Employees
                .AsNoTracking()
                .Include(x => x.Lookup)
                .Where(x => 
                    x.IsActive && 
                    x.Id != employeeId &&
                    x.Lookup.MajorCode == (int)LookupMajorCodes.EmployeePositions && 
                    x.Lookup.MinorCode == (int)PositionsMinorCode.Manager)
                .Select(x => new ListDto
                {
                    Id = x.Id,
                    Name =x.Name,
                }).ToList());
        }

        [HttpPost("Add")]
        public IActionResult Add([FromForm] SaveEmployeeDto newEmployee)
        {
            var newUser = new User
            {
                UserName = $"{newEmployee.Name}_HR",
                HashedPassword = BCrypt.Net.BCrypt.HashPassword($"{newEmployee.Name}@123"),
                IsAdmin = false
            };
            _dbContext.Add(newUser);

            string? imagePath = null;
            if (newEmployee.Image is not null)
            {
                imagePath = UploadImage(newEmployee.Image);
            }

            _dbContext.Employees.Add(new ()
            {
                Name = newEmployee.Name,
                Phone = newEmployee.Phone,
                BirthDate = newEmployee.BirthDate,
                PositionId = newEmployee.PositionId,
                IsActive = newEmployee.IsActive,
                StartDate = newEmployee.StartDate,
                EndDate = newEmployee.EndDate,
                DepartmentId = newEmployee.DepartmentId,
                ManagerId = newEmployee.ManagerId,
                ImagePath = imagePath,
                User = newUser // EF will automatically reference the UserId in the new User record above
            });

            _dbContext.SaveChanges(); // It won't save the added changes unless this method is called
            return Created();
        }

        [HttpPut("Update")]
        public IActionResult Update([FromForm] SaveEmployeeDto updateEmployee)
        {
            var targetEmployee = _dbContext.Employees
                .FirstOrDefault(employee => employee.Id == updateEmployee.Id);

            if (targetEmployee is null)
            {
                return BadRequest($"Employee {updateEmployee.Id} Not Found");
            }

            targetEmployee.Name = updateEmployee.Name;
            targetEmployee.Phone = updateEmployee.Phone;
            targetEmployee.BirthDate = updateEmployee.BirthDate;
            targetEmployee.PositionId = updateEmployee.PositionId;
            targetEmployee.IsActive = updateEmployee.IsActive;
            targetEmployee.StartDate = updateEmployee.StartDate;
            targetEmployee.EndDate = updateEmployee.EndDate;
            targetEmployee.DepartmentId = updateEmployee.DepartmentId;
            targetEmployee.ManagerId = updateEmployee.ManagerId;

            if (updateEmployee.Image is not null)
            {
                targetEmployee.ImagePath = UploadImage(updateEmployee.Image);
            }
            else if (updateEmployee.Image is null && updateEmployee.HaveImage is false)
            {
                targetEmployee.ImagePath = null;
            }

            _dbContext.Employees.Update(targetEmployee);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] long id)
        {
            try
            {
                var targetEmployee = _dbContext.Employees
                        .FirstOrDefault(employee => employee.Id == id);

                if (targetEmployee is null)
                    return BadRequest($"Employee {id} Not Found");

                var employeeAssociate = _dbContext.Employees
                    .FirstOrDefault(x => x.ManagerId == id);

                if (employeeAssociate != null) 
                    return BadRequest("Managers with assigned employees cannot be deleted.");

                _dbContext.Employees.Remove(targetEmployee);
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        private string UploadImage(IFormFile image)
        {
            var wwwrootPath = _env.WebRootPath;
            var attachmentsPath = Path.Combine("Attachments", "EmployeeImages");

            var folderPath = Path.Combine(wwwrootPath, attachmentsPath);

            Directory.CreateDirectory(folderPath); // Checks for directory and creates if it doesnt exist already

            var fileExtension = Path.GetExtension(image.FileName);
            var fileName = Guid.NewGuid() + fileExtension;
            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            image.CopyTo(stream);

            return Path.Combine(attachmentsPath, fileName);
        }
    }
}
