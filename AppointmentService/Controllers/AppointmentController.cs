using AppointmentService.DTOs;
using AppointmentService.Models;
using AppointmentService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace AppointmentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _service;

    public AppointmentController(IAppointmentService service)
    {
        _service = service;
    }

    // 🔥 Patient books appointment
    [Authorize(Roles = "Patient")]
    [HttpPost]
    public async Task<IActionResult> BookAppointment(BookAppointmentDto dto)
    {
        var userId = User.FindFirst("UserId")?.Value;

        var appointment = new Appointment
        {
            PatientId = int.Parse(userId!),
            DoctorId = dto.DoctorId,
            AppointmentDate = dto.AppointmentDate,
            Status = "Booked"
        };

        await _service.BookAppointment(appointment);

        return Ok(new
        {
            message = "Appointment booked successfully"
        });
    }

    // 🔥 Any logged-in user can view
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAppointments()
    {
        return Ok(await _service.GetAppointments());
    }

    // 🔥 Doctor updates status
    [HttpPut("updatestatus/{id}")]

    [Authorize(Roles = "Doctor")]

    public async Task<IActionResult>
    UpdateStatus(
        int id,
        [FromBody] string status
    )
    {
        var updated =
            await _service
                .UpdateAppointmentStatus(
                    id,
                    status
                );

        if (!updated)
        {
            return NotFound();
        }

        return Ok(new
        {
            message =
                "Appointment status updated"
        });
    }

    // get appointments
    [HttpGet("myappointments")]

    [Authorize(Roles = "Patient")]

    public async Task<IActionResult> GetMyAppointments()
    {
        var userIdClaim =
            User.FindFirst("UserId")?.Value;

        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        int userId = int.Parse(userIdClaim);

        var appointments =
            await _service.GetMyAppointments(userId);

        return Ok(appointments);
    }

    [HttpGet("doctorappointments")]

    [Authorize(Roles = "Doctor")]

    public async Task<IActionResult>
    GetDoctorAppointments()
    {
        var userIdClaim =
            User.FindFirst("UserId")?.Value;

        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        int doctorId =
            int.Parse(userIdClaim);

        var appointments =
            await _service
                .GetDoctorAppointments(doctorId);

        return Ok(appointments);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAppointments()
    {
        var appointments = await _service.GetAppointments();
        return Ok(appointments);
    }

    [HttpPut("cancel/{id}")]

    [Authorize(Roles = "Patient")]

    public async Task<IActionResult>
    CancelAppointment(int id)
    {
        var updated =
            await _service
                .UpdateAppointmentStatus(
                    id,
                    "Cancelled"
                );

        if (!updated)
        {
            return NotFound();
        }

        return Ok(new
        {
            message =
                "Appointment cancelled"
        });
    }

    
}