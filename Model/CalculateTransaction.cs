using System;


namespace ExpenseTracker.Model
{
    public class CalculateTransaction
    {
        public decimal InflowAmount { get; set; }
        public decimal TotalOutflow { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal PendingDebt { get; set; }

        public decimal ClearedDebt { get; set; }

        public decimal ClearedInflow { get; set; }
    }
}
