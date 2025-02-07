using Patient_Manager.Models;

namespace Patient_Manager.IRepository
{

    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription> GetPrescriptionByIdAsync(int id);
        Task AddPrescriptionAsync(Prescription prescription);
        Task UpdatePrescriptionAsync(Prescription prescription);
        Task DeletePrescriptionAsync(int id);
    }
}
