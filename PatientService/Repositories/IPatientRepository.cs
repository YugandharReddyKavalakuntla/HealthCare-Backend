using PatientService.Models;

namespace PatientService.Repository;

public interface IPatientRepository
{
    Task Add(Patient patient);
    Task<List<Patient>> GetAll();

    Task<Patient?>GetByUserId(int userId);

    Task UpdatePatient(Patient patient);


}