using UserService.Models;

namespace UserService.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByUsername(string username);
    Task AddUser(User user);

    Task<User?> GetById(int id);
    Task Save();

    Task<IEnumerable<User>> GetPendingDoctors();

    Task<IEnumerable<User>> GetPatients();

    Task DeleteUser(User user);
}