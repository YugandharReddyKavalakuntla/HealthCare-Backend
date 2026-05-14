using System.ComponentModel.DataAnnotations;

namespace PatientService.DTOs;

public class PatientDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 120)]
    public int Age { get; set; }

    [Required]
    public string Gender { get; set; } = string.Empty;
}