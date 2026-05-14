using AppointmentService.Models;

namespace AppointmentService.Repositories;

public interface IAppointmentRepository
{
    Task AddAppointment(Appointment appointment);

    Task<List<Appointment>> GetAllAppointments();

    Task<Appointment?> GetById(int id);

    Task SaveChanges();

    Task<IEnumerable<Appointment>> GetMyAppointments(int userId);
    Task<IEnumerable<Appointment>>GetDoctorAppointments(int doctorId);
    Task<bool>UpdateAppointmentStatus(int appointmentId,string status);


}