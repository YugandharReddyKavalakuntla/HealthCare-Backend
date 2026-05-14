using UserService.DTOs;
using UserService.Models;

namespace UserService.Services;

public interface IUserService
{
    Task<string> Register(RegisterDto dto);
    Task<string> Login(LoginDto dto);
    Task<bool> ApproveDoctor(int id);
    Task<IEnumerable<User>>GetPendingDoctors();

    Task<IEnumerable<User>>GetPatients();

    Task<bool>DeletePatient(int id);
}