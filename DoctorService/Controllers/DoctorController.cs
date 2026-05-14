using DoctorService.DTOs;
using DoctorService.Models;
using DoctorService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoctorService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _service;

    public DoctorController(IDoctorService service)
    {
        _service = service;
    }

    // 🔥 Only Admin can add doctors
    [Authorize(Roles = "Doctor,Admin")]
    [HttpPost]
    public async Task<IActionResult> AddDoctor(CreateDoctorDto dto)
    {
        var userId = User.FindFirst("UserId")?.Value;

        var doctor = new Doctor
        {
            Name = dto.Name,
            Specialization = dto.Specialization,
            Experience = dto.Experience,
            UserId = int.Parse(userId!)
        };

        await _service.AddDoctor(doctor);

        // return Ok("Doctor added successfully");
        return Ok(new
        {
            message =
        "Doctor added successfully"
        });
    }

    // 🔥 Any authenticated user can view doctors
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetDoctors()
    {
        return Ok(await _service.GetDoctors());
    }

    [HttpPut("{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> UpdateDoctor(int id, Doctor doctor)
    {
        var updated =
            await _service
                .UpdateDoctor(id, doctor);

        if (!updated)
        {
            return NotFound();
        }

        return Ok(new
        {
            message =
                "Doctor updated"
        });
    }

    [HttpPut("update")]

    [Authorize(Roles = "Doctor")]

    public async Task<IActionResult>
    UpdateMyProfile(
        Doctor doctor
    )
    {
        var userIdClaim =
            User.FindFirst("UserId")?.Value;

        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        int userId =
            int.Parse(userIdClaim);

        var updated =
            await _service
            .UpdateMyProfile( userId, doctor );
                // .UpdateDoctor(
                //     userId,
                //     doctor
                // );

        if (!updated)
        {
            return NotFound();
        }

        return Ok(new
        {
            message =
                "Doctor updated successfully"
        });
    }







    [HttpDelete("{id}")]

    [Authorize(Roles = "Admin")]

    public async Task<IActionResult>
DeleteDoctor(int id)
    {
        var deleted =
            await _service
                .DeleteDoctor(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok(new
        {
            message =
                "Doctor deleted"
        });
    }

    [HttpGet("myprofile")]

    [Authorize(Roles = "Doctor")]

    public async Task<IActionResult>
GetMyProfile()
    {
        var userIdClaim =
            User.FindFirst("UserId")?.Value;

        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        int userId =
            int.Parse(userIdClaim);

        var profile =
            await _service
                .GetMyProfile(userId);

        if (profile == null)
        {
            return NotFound();
        }

        return Ok(profile);
    }





}