using Patient_Manager.Models;

namespace Patient_Manager.IRepository
{
    public interface ICheckupRepository
    {
        Task<IEnumerable<Checkup>> GetAllCheckupsAsync();
        Task<Checkup> GetCheckupByIdAsync(int id);
        Task AddCheckupAsync(Checkup checkup);
        Task UpdateCheckupAsync(Checkup checkup);
        Task DeleteCheckupAsync(int id);
    }
}
