namespace ExpenseManager.Entities
{
    public class Summary
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string SummaryType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Summary(int userId, string summaryType, decimal amount, DateTime date)
        {
            this.userId = userId;
            SummaryType = summaryType;
            Amount = amount;
            Date = date;
        }
    }
}
