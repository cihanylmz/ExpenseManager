namespace ExpenseManager.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Transaction>? Expenses { get; set; }
        public decimal? DailyExpense { get; set; }
        public decimal? WeeklyExpense { get; set;}
        public decimal? MonthlyExpense { get; set; }
    }
}
