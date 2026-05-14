using AppointmentService.Models;
using AppointmentService.Repositories;

namespace AppointmentService.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task BookAppointment(Appointment appointment)
    {
        if (appointment.AppointmentDate < DateTime.Now)
        {
            throw new Exception(
                "Past appointments are not allowed"
            );
        }

        await _repository.AddAppointment(appointment);
    }

    public async Task<List<Appointment>> GetAppointments()
    {
        return await _repository.GetAllAppointments();
    }

    public async Task<bool> UpdateStatus(int id, string status)
    {
        var appointment = await _repository.GetById(id);

        if (appointment == null)
            return false;

        appointment.Status = status;

        await _repository.SaveChanges();

        return true;
    }

    public async Task<IEnumerable<Appointment>>
        GetMyAppointments(int userId)
    {
        return await _repository
            .GetMyAppointments(userId);
    }

    public async Task<IEnumerable<Appointment>>
    GetDoctorAppointments(int doctorId)
    {
        return await _repository
            .GetDoctorAppointments(doctorId);
    }

    // public async Task<bool>
    // UpdateAppointmentStatus(
    //     int appointmentId,
    //     string status
    // )
    // {
    //     return await _repository
    //         .UpdateAppointmentStatus(
    //             appointmentId,
    //             status
    //         );
    // }

    public async Task<bool>
    UpdateAppointmentStatus(
        int appointmentId,
        string status
    )
    {
        var appointment =
            await _repository
                .GetById(appointmentId);

        if (appointment == null)
        {
            return false;
        }

        // cannot modify completed
        if (appointment.Status == "Completed")
        {
            throw new Exception(
                "Completed appointment cannot be modified"
            );
        }

        // cannot complete cancelled
        if (
            appointment.Status == "Cancelled"
            &&
            status == "Completed"
        )
        {
            throw new Exception(
                "Cancelled appointment cannot be completed"
            );
        }

        appointment.Status = status;

        await _repository.SaveChanges();

        return true;
    }


}