using AppointmentService.Data;
using AppointmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentService.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAppointment(Appointment appointment)
    {
        _context.Appointments.Add(appointment);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Appointment>> GetAllAppointments()
    {
        return await _context.Appointments.ToListAsync();
    }

    // public async Task<Appointment?> GetById(int id)
    // {
    //     return await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
    // }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Appointment>>
        GetMyAppointments(int userId)
    {
        return await _context.Appointments
            .Where(a => a.PatientId == userId)
            .ToListAsync();
    }


    public async Task<IEnumerable<Appointment>>
    GetDoctorAppointments(int doctorId)
    {
        // return await _context.Appointments
        //     .Where(a => a.DoctorId == doctorId)
        //     .ToListAsync();
        return await _context.Appointments
    .ToListAsync();
    }




    public async Task<bool>
        UpdateAppointmentStatus(
            int appointmentId,
            string status
        )
    {
        var appointment =
            await _context.Appointments
                .FindAsync(appointmentId);

        if (appointment == null)
        {
            return false;
        }

        appointment.Status = status;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Appointment?>
    GetById(int id)
    {
        return await _context.Appointments
            .FindAsync(id);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }


}