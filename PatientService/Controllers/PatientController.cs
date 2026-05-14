using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientService.Models;
using PatientService.Services;
using System.Security.Claims;


namespace PatientService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _service;

    public PatientController(IPatientService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddPatient(Patient patient)
    {
        // 🔥 Extract user info from JWT
        // var userId = User.FindFirst("UserId")?.Value;

        // await _service.AddPatient(patient);

        // return Ok("Patient added");

        var userId = User.FindFirst("UserId")?.Value;

        patient.UserId = int.Parse(userId);

        await _service.AddPatient(patient);

        // return Ok("Patient added");
        return Ok(new
        {
            message =
        "Patient added successfully"
        });
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetPatients());
    }

    [HttpGet("myprofile")]

    [Authorize(Roles = "Patient")]

    public async Task<IActionResult> GetMyProfile()
    {
        var userIdClaim =
            User.FindFirst("UserId")?.Value;

        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        int userId =
            int.Parse(userIdClaim);

        var patient =
            await _service
                .GetMyProfile(userId);

        if (patient == null)
        {
            return NotFound();
        }

        return Ok(patient);
    }

    [HttpPut("update")]

    [Authorize(Roles = "Patient")]

    public async Task<IActionResult> UpdatePatient(Patient patient)
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
                .UpdatePatient(
                    userId,
                    patient
                );

        if (!updated)
        {
            return NotFound();
        }

        return Ok(new
        {
            message =
                "Patient updated successfully"
        });
    }






}




