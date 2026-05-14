using DoctorService.Models;

namespace DoctorService.Repositories;

public interface IDoctorRepository
{
    Task AddDoctor(Doctor doctor);

    Task<List<Doctor>> GetAllDoctors();

    // Task<IEnumerable<Doctor>GetAllDoctors();

    Task<Doctor?> GetById(int id);

    Task UpdateDoctor(Doctor doctor);

    Task DeleteDoctor(Doctor doctor);

    Task<Doctor?>GetByUserId(int userId);



}