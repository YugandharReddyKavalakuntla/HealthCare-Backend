namespace PatientService.Models;

public class Patient
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public string Gender { get; set; }

    public int UserId { get; set; } // from JWT
}