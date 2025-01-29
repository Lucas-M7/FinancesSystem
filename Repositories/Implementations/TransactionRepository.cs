using API.Data;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    private readonly FinanceAppContext _context;
    
    public TransactionRepository(FinanceAppContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transaction>> FindByTransactionIdAsync(int transactionId)
    {
        return await _context.Transactions
            .Include(t => t.User)
            .Include(t => t.Category)
            .ToListAsync();
    }

    public async Task AddTransactionAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTransactionAsync(Transaction transaction)
    {
        var existingTransaction = await _context.Transactions.FindAsync(transaction.Id);
        
        if (existingTransaction == null)
            throw new KeyNotFoundException("Transacion not found");
        
        existingTransaction.Amount = transaction.Amount;
        existingTransaction.Description = transaction.Description;
        existingTransaction.CategoryId = transaction.CategoryId;
        existingTransaction.Date = transaction.Date;
        existingTransaction.UserId = transaction.UserId;
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTransactionAsync(int transactionId)
    {
        var transactionToDelete = await _context.Transactions.FindAsync(transactionId);
        
        if (transactionToDelete == null)
            throw new KeyNotFoundException("Transacion not found");
        
        _context.Transactions.Remove(transactionToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
    {
        return await _context.Transactions
            .Include(t => t.User)
            .Include(t => t.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsByCategoryIdAsync(int categoryId)
    {
        return await _context.Transactions
            .Where(t => t.CategoryId == categoryId)
            .Include(t => t.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsByUserIdAsync(string userId)
    {
        return await _context.Transactions
            .Where(t => t.UserId == userId)
            .Include(t => t.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Transactions
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .Include(t => t.User)
            .Include(t => t.Category)
            .ToListAsync();
    }
}