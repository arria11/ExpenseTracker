using ExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public interface ITransactionService
    {
        Task SaveTransactionAsync(TransactionItem transactionItem);
        Task UpdateTransactionAsync(TransactionItem transactionItem);

        Task DeleteTransactionAsync(Guid TransactionId);
        Task<List<TransactionItem>> LoadTransactionAsync();
    }
}
