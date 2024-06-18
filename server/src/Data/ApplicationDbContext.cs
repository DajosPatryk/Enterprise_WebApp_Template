namespace server.Data;

using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Entities;

public class ApplicationDbContext : DbContext
{
    /**
     * Database Context Structure
     */
    public DbSet<User> Users { get; set; }
    
    /**
     * Database Context Logic
     */
    private IDbContextTransaction? _currentTransaction = null;
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public bool HasActiveTransaction => _currentTransaction != null;
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;
        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch(Exception ex)
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
