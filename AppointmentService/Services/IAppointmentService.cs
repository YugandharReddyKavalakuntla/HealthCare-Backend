using AppointmentService.Models;

namespace AppointmentService.Services;

public interface IAppointmentService
{
    Task BookAppointment(Appointment appointment);

    Task<List<Appointment>> GetAppointments();

    Task<bool> UpdateStatus(int id, string status);

    Task<IEnumerable<Appointment>>GetMyAppointments(int userId);

    Task<IEnumerable<Appointment>>GetDoctorAppointments(int doctorId);

    Task<bool>UpdateAppointmentStatus(int appointmentId,string status);

    // Task<Appointment?>GetById(int id);

    // Task Save();

}