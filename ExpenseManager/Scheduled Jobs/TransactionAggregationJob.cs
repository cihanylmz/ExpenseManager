namespace ExpenseManager.Scheduled_Jobs
{
    public class TransactionAggregationJob
    {
        private readonly ApplicationDbContext _context;

        TransactionAggregationJob(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AggregateExpenses()
        {
            Console.Write("Aggregate");
            // Aggregate expenses logic here
            // You can access the database context (_context) to perform operations
        }
    }
}
