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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetExpenses()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var transactions = await _context.Transactions
            .Where(e => e.TransactionUserID == userId.ToString())
            .ToListAsync();

        return transactions;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetTransaction(int id)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var expense = await _context.Transactions
            .Where(e => e.TransactionUserID == userId.ToString() && e.TransactionId == id.ToString())
            .FirstOrDefaultAsync();

        if (expense == null)
        {
            return NotFound();
        }

        return expense;
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> CreateTransaction(Transaction transaction)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        transaction.TransactionUserID = userId.ToString();

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTransaction), new { id = transaction.TransactionId }, transaction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, Transaction transaction)
    {
        if (id.ToString() != transaction.TransactionId)
        {
            return BadRequest();
        }

        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var existingTransaction = await _context.Transactions
            .Where(e => e.TransactionUserID == userId.ToString() && e.TransactionId == id.ToString())
            .FirstOrDefaultAsync();

        if (existingTransaction == null)
        {
            return NotFound();
        }

        transaction.TransactionId = transaction.TransactionId;
        existingTransaction.TransactionAmount = transaction.TransactionAmount;
        existingTransaction.Date = transaction.Date;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletTransactione(int id)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var transaction = await _context.Transactions
            .Where(e => e.TransactionUserID == userId.ToString() && e.TransactionId == id.ToString())
            .FirstOrDefaultAsync();

        if (transaction == null)
        {
            return NotFound();
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return NoContent();
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