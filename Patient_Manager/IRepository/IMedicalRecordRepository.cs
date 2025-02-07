using Patient_Manager.Models;

namespace Patient_Manager.IRepository
{
   public interface IMedicalRecordRepository
{
    Task<IEnumerable<Medicalrecord>> GetAllMedicalRecordsAsync();
    Task<Medicalrecord> GetMedicalRecordByIdAsync(int id);
    Task AddMedicalRecordAsync(Medicalrecord medicalRecord);
    Task UpdateMedicalRecordAsync(Medicalrecord medicalRecord);
    Task DeleteMedicalRecordAsync(int id);
}
}
