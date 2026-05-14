using System.ComponentModel.DataAnnotations;

namespace UserService.DTOs;

public class RegisterDto
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(3)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;
}