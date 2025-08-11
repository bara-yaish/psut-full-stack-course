using HR.DTOs.Vacations;
using HR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR.Controllers
{
    [Authorize] // Add Security
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsController(HrDbContext dbContext) : ControllerBase
    {
        private readonly HrDbContext _dbContext = dbContext;

        [HttpGet]
        public IActionResult GetAll([FromQuery] FilterVacationDto filters)
        {
            return Ok(
                _dbContext.Vacations
                .AsNoTracking()
                .Where(x => x.TypeId == filters.VacationTypeId || filters.VacationTypeId == null)
                .Where(x => x.EmployeeId == filters.EmployeeId || filters.EmployeeId == null)
                .Select(x => new VacationDto
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.Name,
                    CreationDate = x.CreationDate,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    TypeId = x.TypeId,
                    TypeName = x.Lookup.Name,
                    Notes = x.Notes,
                })
                .OrderBy(x => x.CreationDate));
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] long id)
        {
            var vacation = _dbContext.Vacations
                .AsNoTracking()
                .Select(x => new VacationDto
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.Name,
                    CreationDate = x.CreationDate,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    TypeId = x.TypeId,
                    TypeName = x.Lookup.Name,
                    Notes = x.Notes,

                })
                .SingleOrDefault(x => x.Id == id);

            if (vacation == null) return NotFound(new { Message = $"Vacation with ID {id} not found" });

            return Ok(vacation);
        }
        
        [HttpPost]
        public IActionResult Add([FromBody] SaveVacationDto newVacation)
        {
            try
            {
                _dbContext.Vacations
                .Add(new Vacation
                {
                    StartDate = newVacation.StartDate,
                    EndDate = newVacation.EndDate,
                    Notes = newVacation.Notes,
                    EmployeeId = newVacation.EmployeeId,
                    TypeId = newVacation.TypeId,
                });
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return Conflict(new { ex.GetType().Name, ex.Message, Details = ex?.InnerException?.Message });
            }

            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] long id, [FromBody] SaveVacationDto updateVacation)
        {
            try
            {
                var targetVacation = _dbContext.Vacations.SingleOrDefault(x => x.Id == id);

                if (targetVacation == null) return NotFound(new { Message = $"Vacation with ID {id} not found" });

                targetVacation.StartDate = updateVacation.StartDate;
                targetVacation.EndDate = updateVacation.EndDate;
                targetVacation.Notes = updateVacation.Notes;
                targetVacation.EmployeeId = updateVacation.EmployeeId;
                targetVacation.TypeId = updateVacation.TypeId;

                _dbContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return Conflict(new { ex.GetType().Name, ex.Message, Details = ex?.InnerException?.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id) 
        {
            var vacation = _dbContext.Vacations.SingleOrDefault(x => x.Id == id);

            if (vacation == null) return NotFound(new { Message = $"Vacation with ID {id} not found" });

            try
            {
                _dbContext.Vacations.Remove(vacation);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return Conflict(new { ex.GetType().Name, ex.Message, Details = ex?.InnerException?.Message });
            }

            return NoContent();
        }
    }
}
