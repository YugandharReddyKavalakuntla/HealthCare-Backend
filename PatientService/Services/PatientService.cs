using PatientService.Repository;
using PatientService.Models;

namespace PatientService.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repo;

    public PatientService(IPatientRepository repo)
    {
        _repo = repo;
    }

    public async Task AddPatient(Patient patient)
    {
        await _repo.Add(patient);
    }

    public async Task<List<Patient>> GetPatients()
    {
        return await _repo.GetAll();
    }

    public async Task<Patient?> GetMyProfile(int userId)
    {
        return await _repo
            .GetByUserId(userId);
    }

    public async Task<bool>UpdatePatient(int userId,Patient updatedPatient)
    {
        var patient =
            await _repo
                .GetByUserId(userId);

        if (patient == null)
        {
            return false;
        }

        patient.Name =
            updatedPatient.Name;

        patient.Age =
            updatedPatient.Age;

        patient.Gender =
            updatedPatient.Gender;

        await _repo
            .UpdatePatient(patient);

        return true;
    }



}