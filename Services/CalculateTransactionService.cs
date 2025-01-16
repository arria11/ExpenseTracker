using ExpenseTracker.Model;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Services
{
    public class CalculateTransactionService
    {

        public CalculateTransaction CalculateTransactionTotals(List<TransactionItem> transactions)
        {
            if (transactions == null || !transactions.Any())
            {
                // Handle empty or null transaction list
                return new CalculateTransaction(); 
            }
            //Calculating the transaction amounts using Lamdba expressions and linq methods
            var totalInflow = transactions.Where(t => t.Type == "Inflow").Sum(t => t.Amount);
            var totalOutflow = transactions.Where(t => t.Type == "Outflow").Sum(t => t.Amount);
            var totalDebt = transactions.Where(t => t.Type == "Debt").Sum(t => t.Amount);
            var clearedDebt = transactions.Where(t => t.Type == "Debt" && t.IsCleared).Sum(t => t.Amount);

            var pendingDebt = totalDebt - clearedDebt;
            var netBalance = totalInflow - totalOutflow + pendingDebt;
            var availableBalance = totalInflow - totalOutflow;

            //returning the calculated values
            return new CalculateTransaction
            {
                TotalInflow = totalInflow,
                TotalOutflow = totalOutflow,
                TotalDebt = totalDebt,
                PendingDebt = pendingDebt,
                ClearedDebt = clearedDebt,
                NetBalance = netBalance,
                AvailableBalance = availableBalance
            };
        }
    }
}
