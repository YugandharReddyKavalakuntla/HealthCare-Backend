using PatientService.Models;
using PatientService.Data;
using Microsoft.EntityFrameworkCore;

namespace PatientService.Repository;
public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Patient>> GetAll()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient?> GetByUserId(int userId)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(
                p => p.UserId == userId
            );
    }

    public async Task UpdatePatient(Patient patient)
    {
        _context.Patients
            .Update(patient);

        await _context
            .SaveChangesAsync();
    }



}