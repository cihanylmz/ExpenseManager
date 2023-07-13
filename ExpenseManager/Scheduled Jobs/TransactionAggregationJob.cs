using ExpenseManager.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Scheduled_Jobs
{
    public class TransactionAggregationJob
    {
        private readonly ApplicationDbContext _context;

        public TransactionAggregationJob(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<decimal>> AggregateExpenses()
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                user.DailyExpense = await _context.Transactions
                .Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-1))
                .SumAsync(e => e.TransactionAmount);

                user.WeeklyExpense = await _context.Transactions
                .Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-7))
                .SumAsync(e => e.TransactionAmount);

                user.MonthlyExpense = await _context.Transactions
                .Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-30))
                .SumAsync(e => e.TransactionAmount);

                _context.Users.Update(user);
            }
            _context.SaveChanges();
            return 1;
        }
    }
}
