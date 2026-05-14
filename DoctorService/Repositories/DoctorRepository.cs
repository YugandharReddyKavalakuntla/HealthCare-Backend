using DoctorService.Data;
using DoctorService.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;

    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Doctor>> GetAllDoctors()
    {
        return await _context.Doctors.ToListAsync();
    }

    public async Task<Doctor?> GetById(int id)
    {
        return await _context.Doctors
            .FindAsync(id);
    }

    public async Task UpdateDoctor(Doctor doctor)
    {
        _context.Doctors.Update(doctor);

        await _context.SaveChangesAsync();
    }

    

    public async Task DeleteDoctor(Doctor doctor)
    {
        _context.Doctors.Remove(doctor);

        await _context.SaveChangesAsync();
    }

    public async Task<Doctor?> GetByUserId(int userId)
    {
        return await _context.Doctors
            .FirstOrDefaultAsync(
                d => d.UserId == userId
            );
    }






}