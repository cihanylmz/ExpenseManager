namespace ExpenseManager.Entities
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string TransactionId { get; set; }
        public string TransactionPlace { get; set; }  
        public int TransactionAmount { get; set; }
        public string TransactionUserID { get; set; }
    }
    
}