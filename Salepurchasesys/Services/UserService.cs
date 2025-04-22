using Microsoft.EntityFrameworkCore;
using SalePurchasesys.Data;
using SalePurchasesys.Models;
// …

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    public UserService(ApplicationDbContext context) { _context = context; }

    public async Task<IEnumerable<User>> GetAllUsersAsync() =>
        await _context.Users.ToListAsync();

    public async Task<User> GetUserByIdAsync(int id) =>
        await _context.Users.FindAsync(id);

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(int id, User user)
    {
        var existing = await _context.Users.FindAsync(id);
        if (existing == null) return null;
        existing.Name = user.Name;
        existing.Email = user.Email;
        existing.Address = user.Address;
        existing.Role = user.Role;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var u = await _context.Users.FindAsync(id);
        if (u == null) return false;
        _context.Users.Remove(u);
        await _context.SaveChangesAsync();
        return true;
    }

    // ← NEW
    public async Task<User> GetUserByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
}
