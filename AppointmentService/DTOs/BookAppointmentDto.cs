using System.ComponentModel.DataAnnotations;

namespace AppointmentService.DTOs;

public class BookAppointmentDto
{
    [Range(1, int.MaxValue)]
    public int DoctorId { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }
}