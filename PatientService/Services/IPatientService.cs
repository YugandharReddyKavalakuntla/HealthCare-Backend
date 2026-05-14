using PatientService.Models;
namespace PatientService.Services;
public interface IPatientService
{
    Task AddPatient(Patient patient);
    Task<List<Patient>> GetPatients();
    Task<Patient?> GetMyProfile(int userId);

    Task<bool> UpdatePatient(int userId,Patient updatedPatient);



}