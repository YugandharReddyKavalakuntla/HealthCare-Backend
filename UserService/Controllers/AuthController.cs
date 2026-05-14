using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Services;
using Microsoft.AspNetCore.Authorization;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _service;

    public AuthController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _service.Register(dto);
        return Ok(new
        {
            message =
        "User Registered"
        });
    }

    // [HttpPost("login")]
    // public async Task<IActionResult> Login(LoginDto dto)
    // {
    //     // throw new Exception("Test exception");

    //     var result = await _service.Login(dto);
    //     return Ok(result);
    // }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _service.Login(dto);

        if (result == "Invalid credentials")
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }

    [Authorize]
    [HttpGet("secure")]
    public IActionResult Secure()
    {
        return Ok("Authenticated user");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
    {
        return Ok("Admin access granted");
    }

    [Authorize(Roles = "Doctor")]
    [HttpGet("doctor-only")]
    public IActionResult DoctorOnly()
    {
        return Ok("Doctor access granted");
    }

    [Authorize(Roles = "Patient")]
    [HttpGet("patient-only")]
    public IActionResult PatientOnly()
    {
        return Ok("Patient access granted");
    }

    [HttpPut("approve-doctor/{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult>
    ApproveDoctor(int id)
    {
        var approved =
            await _service
                .ApproveDoctor(id);

        if (!approved)
        {
            return NotFound();
        }

        return Ok(new
        {
            message =
                "Doctor approved successfully"
        });
    }

    [HttpGet("pending-doctors")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> GetPendingDoctors()
    {
        var doctors =
            await _service
                .GetPendingDoctors();

        return Ok(doctors);
    }

    [HttpGet("patients")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> GetPatients()
    {
        var patients =
            await _service
                .GetPatients();

        return Ok(patients);
    }

    [HttpDelete("patients/{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> DeletePatient(int id)
    {
        var deleted =
            await _service
                .DeletePatient(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok(new
        {
            message =
                "Patient deleted"
        });
    }




}