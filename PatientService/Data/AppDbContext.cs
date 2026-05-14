using Microsoft.EntityFrameworkCore;
using PatientService.Models;

namespace PatientService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
}