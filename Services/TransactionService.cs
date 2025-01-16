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
        // Path to the file where transactions are stored
        private readonly string filePath = Path.Combine(AppContext.BaseDirectory, "Transaction.json");

        // Load transactions from JSON file 
        public async Task<List<TransactionItem>> LoadTransactionAsync()
        {
            try
            {
                // Check if the file exists
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

        // Saves a new transaction by adding it to the existing list
        public async Task SaveTransactionAsync(TransactionItem transactionItem)
        {
            try
            {
                var transactions = await LoadTransactionAsync();
                // Add the new transaction to the list
                transactions.Add(transactionItem);
                // Save the updated transaction list
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
                var existingTransaction = transactions.FirstOrDefault(t => t.TransactionId == transactionItem.TransactionId);
                
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

        //Saving the transaction list to the JSON file.
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

        // Deletes a transaction from the file using the transactionID.
        public async Task DeleteTransactionAsync(Guid transactionId)
        {
            try
            {
                var transactions = await LoadTransactionAsync();

                //the transaction id is retrieved
                var transactionToDelete = transactions.FirstOrDefault(t => t.TransactionId == transactionId);

                if (transactionToDelete != null)
                {
                    transactions.Remove(transactionToDelete); //Removing the transaction.
                    await SaveTransactionAsync(transactions); //Saving the list removing the deleted transaction.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


    }
}
