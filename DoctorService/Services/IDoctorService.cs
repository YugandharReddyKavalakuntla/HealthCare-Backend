using DoctorService.Models;

namespace DoctorService.Services;

public interface IDoctorService
{
    Task AddDoctor(Doctor doctor);

    Task<List<Doctor>> GetDoctors();

    Task<bool>UpdateDoctor(int id,Doctor doctor);

    Task<bool>DeleteDoctor(int id);

    Task<Doctor?>GetMyProfile(int userId);

    Task<bool> UpdateMyProfile(int userId, Doctor doctor);



}