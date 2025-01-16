using System;


namespace ExpenseTracker.Model
{
    public class CalculateTransaction
    {
        public decimal TotalInflow { get; set; }
        public decimal TotalOutflow { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal PendingDebt { get; set; }

        public decimal ClearedDebt { get; set; }

        public decimal NetBalance { get; set; }

        public decimal AvailableBalance { get; set; }


    }
}
