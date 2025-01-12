using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ExpenseTracker.Model;

namespace ExpenseTracker.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly string filePath = Path.Combine(AppContext.BaseDirectory, "Transaction.json");

        public async Task<List<TransactionItem>> LoadTransactionAsync()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<TransactionItem>();
                }

                var json = await File.ReadAllTextAsync(filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<TransactionItem>();
                }

                return JsonSerializer.Deserialize<List<TransactionItem>>(json) ?? new List<TransactionItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task SaveTransactionAsync(TransactionItem transactionItem)
        {
            try
            {
                var transactions = await LoadTransactionAsync();
                transactions.Add(transactionItem);
                await SaveTransactionAsync(transactions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task UpdateTransactionAsync(TransactionItem transactionItem)
        {
            try
            {
                var transactions = await LoadTransactionAsync();
                var existingTransaction = transactions.FirstOrDefault(t => t.TaskId == transactionItem.TaskId);

                if (existingTransaction != null)
                {
                    existingTransaction.IsCleared = transactionItem.IsCleared;
                    existingTransaction.Title = transactionItem.Title;
                    existingTransaction.Amount = transactionItem.Amount;
                    existingTransaction.Tag = transactionItem.Tag;
                    existingTransaction.CustomTag = transactionItem.CustomTag;
                    existingTransaction.Date = transactionItem.Date;
                    existingTransaction.Note = transactionItem.Note;
                    existingTransaction.Type = transactionItem.Type;
                    existingTransaction.DueDate = transactionItem.DueDate;
                    existingTransaction.Source = transactionItem.Source;

                    await SaveTransactionAsync(transactions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }




        private async Task SaveTransactionAsync(List<TransactionItem> transactions)
        {
            try
            {
                var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });

                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



    }
}
