using HR.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController(HrDbContext dbContext) : ControllerBase
    {
        private HrDbContext _dbContext = dbContext;

        [HttpGet("GetByMajorCode")]
        public IActionResult GetByMajorCode([FromQuery] int majorCode)
        {
            try
            {
                return Ok(_dbContext.Lookups
                    .AsNoTracking()
                    .Where(x => x.MajorCode == majorCode && x.MinorCode != 0)
                    .Select(x => new ListDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
