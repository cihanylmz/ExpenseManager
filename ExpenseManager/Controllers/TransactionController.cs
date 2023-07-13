using ExpenseManager;
using ExpenseManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TransactionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/expenses/sum
    [HttpGet("sum")]
    public async Task<ActionResult<decimal>> GetSumOfTransactions()
    {

        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        decimal sum = await _context.Transactions
            .Where(e => e.TransactionUserID == userId.ToString())
            .SumAsync(e => e.TransactionAmount);

        return sum;
    }

    // Other CRUD operations (GET, POST, PUT, DELETE) for expenses
}