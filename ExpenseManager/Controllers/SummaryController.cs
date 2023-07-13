using ExpenseManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/summaries")]
    public class SummaryController : Controller
    {

        public class UserController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public UserController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Summary>>> GetSummaries()
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (userId == null)
                {
                    return NotFound();
                }
                var summaries = await _context.Summaries.Where(e => e.userId == userId).ToListAsync();
                return summaries;
            }
        }
    }
}
