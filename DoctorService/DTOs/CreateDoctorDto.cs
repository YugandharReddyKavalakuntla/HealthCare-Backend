using System.ComponentModel.DataAnnotations;

namespace DoctorService.DTOs;

public class CreateDoctorDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Specialization { get; set; } = string.Empty;

    [Range(0, 60)]
    public int Experience { get; set; }
}