using Patient_Manager.Models;

namespace Patient_Manager.IRepository
{
    public interface IMedicalFileRepository
    {
        Task<IEnumerable<Medicalfile>> GetAllMedicalFilesAsync();
        Task<Medicalfile> GetMedicalFileByIdAsync(int id);
        Task AddMedicalFileAsync(Medicalfile medicalFile);
        Task UpdateMedicalFileAsync(Medicalfile medicalFile);
        Task DeleteMedicalFileAsync(int id);
    }
}
