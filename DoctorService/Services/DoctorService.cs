using DoctorService.Models;
using DoctorService.Repositories;

namespace DoctorService.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repository;

    public DoctorService(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public async Task AddDoctor(Doctor doctor)
    {
        await _repository.AddDoctor(doctor);
    }

    public async Task<List<Doctor>> GetDoctors()
    {
        return await _repository.GetAllDoctors();
    }

    public async Task<bool> UpdateDoctor(int id, Doctor updatedDoctor)
    {
        var doctor =
            await _repository
                .GetById(id);

        if (doctor == null)
        {
            return false;
        }

        doctor.Name =
            updatedDoctor.Name;

        doctor.Specialization =
            updatedDoctor.Specialization;

        doctor.Experience =
            updatedDoctor.Experience;

        await _repository
            .UpdateDoctor(doctor);

        return true;
    }

    public async Task<bool> DeleteDoctor(int id)
    {
        var doctor =
            await _repository
                .GetById(id);

        if (doctor == null)
        {
            return false;
        }

        await _repository
            .DeleteDoctor(doctor);

        return true;
    }

    public async Task<Doctor?> GetMyProfile(int userId)
    {
        return await _repository
            .GetByUserId(userId);
    }

    public async Task<bool> UpdateMyProfile( int userId, Doctor updatedDoctor)
    {
        var doctor =
            await _repository
                .GetByUserId(userId);

        if (doctor == null)
        {
            return false;
        }

        doctor.Name =
            updatedDoctor.Name;

        doctor.Specialization =
            updatedDoctor.Specialization;

        doctor.Experience =
            updatedDoctor.Experience;

        await _repository
            .UpdateDoctor(doctor);

        return true;
    }




}