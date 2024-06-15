namespace server.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using server.Entities;


public class UserRepository
{
    private readonly DbContext _context;

    public UserRepository(DbContext context)
    {
        _context = context;
    }
    
    public async Task Add(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
    }
    
    public async Task<User> GetBySub(string sub)
    {
        var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Sub == sub);
        if (user == null) throw new Exception("Error: User not found.");
        return user;
    }
    
    public async Task<List<User>> GetAll()
    {
        return await _context.Set<User>().ToListAsync();
    }
    
    public async Task Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!Exists(user.Sub)) throw new Exception("Error: User not found.");
        }
    }
    
    public async Task Delete(string sub)
    {
        var user = await _context.Set<User>().FindAsync(sub);
        if (user != null)
        {
            _context.Set<User>().Remove(user);
            await _context.SaveChangesAsync();
        }
    }
    
    private bool Exists(string sub)
    {
        return _context.Set<User>().Any(e => e.Sub == sub);
    }
}