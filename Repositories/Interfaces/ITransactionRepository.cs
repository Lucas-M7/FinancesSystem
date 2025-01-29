using API.Models;

namespace API.Repositories.Interfaces;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<IEnumerable<Transaction>> FindByTransactionIdAsync(int transactionId);
    Task AddTransactionAsync(Transaction transaction);
    Task UpdateTransactionAsync(Transaction transaction);
    Task DeleteTransactionAsync(int id);
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    Task<IEnumerable<Transaction>> GetAllTransactionsByCategoryIdAsync(int categoryId);
    Task<IEnumerable<Transaction>> GetAllTransactionsByUserIdAsync(string userId);
    Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
}