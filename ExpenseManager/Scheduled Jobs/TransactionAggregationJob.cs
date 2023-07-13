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

        public async Task<ActionResult<decimal>> AggregateDailyExpenses()
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                var DailyExpense = await _context.Transactions
                .Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-1))
                .SumAsync(e => e.TransactionAmount);

                var summary = new Summary(user.Id, "1", DailyExpense, DateTime.Now);
                //user.WeeklyExpense = await _context.Transactions
                //.Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-7))
                //.SumAsync(e => e.TransactionAmount);

                //user.MonthlyExpense = await _context.Transactions
                //.Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-30))
                //.SumAsync(e => e.TransactionAmount);

                _context.Summaries.Add(summary);
            }
            _context.SaveChanges();
            return 1;
        }

        public async Task<ActionResult<decimal>> AggregateWeeklyExpenses()
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                var WeeklyExpense = await _context.Transactions
                .Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-7))
                .SumAsync(e => e.TransactionAmount);

                var summary = new Summary(user.Id, "2", WeeklyExpense, DateTime.Now);
                //user.WeeklyExpense = await _context.Transactions
                //.Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-7))
                //.SumAsync(e => e.TransactionAmount);

                //user.MonthlyExpense = await _context.Transactions
                //.Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-30))
                //.SumAsync(e => e.TransactionAmount);

                _context.Summaries.Add(summary);
            }
            _context.SaveChanges();
            return 1;
        }

        public async Task<ActionResult<decimal>> AggregateMonthlyExpenses()
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                var MonthlyExpense = await _context.Transactions
                .Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-30))
                .SumAsync(e => e.TransactionAmount);

                var summary = new Summary(user.Id, "3", MonthlyExpense, DateTime.Now);
                //user.WeeklyExpense = await _context.Transactions
                //.Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-7))
                //.SumAsync(e => e.TransactionAmount);

                //user.MonthlyExpense = await _context.Transactions
                //.Where(e => e.TransactionUserID == user.Id.ToString() && e.Date > DateTime.Today.AddDays(-30))
                //.SumAsync(e => e.TransactionAmount);

                _context.Summaries.Add(summary);
            }
            _context.SaveChanges();
            return 1;
        }
    }
}
