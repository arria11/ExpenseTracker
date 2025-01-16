using System;

namespace ExpenseTracker.Model
{
    public class TransactionItem
    {
        public Guid TransactionId { get; set; }

        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string? Tag { get; set; } = string.Empty;
        public string? CustomTag { get; set; } = string.Empty;
        public string? Note { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public string Source { get; set; } = string.Empty;
        public bool IsCleared { get; set; } = false;

    }
}
