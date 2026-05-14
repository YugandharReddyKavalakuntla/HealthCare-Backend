using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;

namespace UserService.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task AddUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?>
    GetById(int id)
    {
        return await _context.Users
            .FindAsync(id);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>>
    GetPendingDoctors()
    {
        return await _context.Users
            .Where(u =>
                u.Role == "Doctor"
                && !u.IsApproved
            )
            .ToListAsync();
    }

    public async Task<IEnumerable<User>>GetPatients()
    {
        return await _context.Users
            .Where(u => u.Role == "Patient")
            .ToListAsync();
    }

    public async Task DeleteUser(User user)
    {
        _context.Users.Remove(user);

        await _context.SaveChangesAsync();
    }
}