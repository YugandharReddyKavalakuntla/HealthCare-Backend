using UserService.DTOs;
using UserService.Models;
using UserService.Repositories;
using Microsoft.Extensions.Logging;

namespace UserService.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly JwtService _jwt;

    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository repo, JwtService jwt, ILogger<UserService> logger)
    {
        _repo = repo;
        _jwt = jwt;
        _logger = logger;
    }


    // public async Task<string> Register(RegisterDto dto)
    // {
    //     var existing = await _repo.GetUserByUsername(dto.Username);

    //     if (existing != null)
    //         return "User already exists";

    //     var user = new User
    //     {
    //         Username = dto.Username,
    //         Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
    //         Role = dto.Role
    //     };

    //     await _repo.AddUser(user);

    //     return "User Registered";
    // }

    public async Task<string> Register(RegisterDto dto)
    {
        _logger.LogInformation(
            "Register attempt for username: {Username}",
            dto.Username);

        var existing = await _repo.GetUserByUsername(dto.Username);

        if (existing != null)
        {
            _logger.LogWarning(
                "Registration failed. Username already exists: {Username}",
                dto.Username);

            return "User already exists";
        }

        // var user = new User
        // {
        //     Username = dto.Username,
        //     Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
        //     Role = dto.Role
        // };
        var user = new User
        {
            Username = dto.Username,

            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),

            Role = dto.Role,

            IsApproved =
                dto.Role == "Patient"
        };

        await _repo.AddUser(user);

        _logger.LogInformation(
            "User registered successfully: {Username}",
            dto.Username);

        return "User Registered";
    }

    // public async Task<string> Login(LoginDto dto)
    // {
    //     var user = await _repo.GetUserByUsername(dto.Username);

    //     if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
    //         return "Invalid credentials";

    //     // 🔥 Generate token
    //     return _jwt.GenerateToken(user);
    // }
    public async Task<string> Login(LoginDto dto)
    {
        _logger.LogInformation(
            "Login attempt for username: {Username}",
            dto.Username);

        var user = await _repo.GetUserByUsername(dto.Username);

        if (user == null ||
            !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            _logger.LogWarning(
                "Invalid login attempt for username: {Username}",
                dto.Username);

            return "Invalid credentials";
        }

        if (!user.IsApproved)
        {
            throw new Exception(
                "Doctor account pending admin approval"
            );
        }

        _logger.LogInformation(
            "Login successful for username: {Username}",
            dto.Username);

        return _jwt.GenerateToken(user);
    }

    public async Task<bool>
    ApproveDoctor(int id)
    {
        var user =
            await _repo
                .GetById(id);

        if (user == null)
        {
            return false;
        }

        user.IsApproved = true;

        await _repo.Save();

        return true;
    }

    public async Task<IEnumerable<User>>
    GetPendingDoctors()
    {
        return await _repo
            .GetPendingDoctors();
    }

    public async Task<IEnumerable<User>>
GetPatients()
{
    return await _repo
        .GetPatients();
}

public async Task<bool>
DeletePatient(int id)
{
    var user =
        await _repo
            .GetById(id);

    if (user == null)
    {
        return false;
    }

    await _repo
        .DeleteUser(user);

    return true;
}






}