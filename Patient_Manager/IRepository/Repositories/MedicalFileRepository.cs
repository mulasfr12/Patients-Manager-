using Microsoft.EntityFrameworkCore;
using Patient_Manager.Data;
using Patient_Manager.Models;

namespace Patient_Manager.IRepository.Repositories
{
    public class MedicalFileRepository : IMedicalFileRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalFileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medicalfile>> GetAllMedicalFilesAsync()
        {
            return await _context.Medicalfiles.ToListAsync();
        }

        public async Task<Medicalfile> GetMedicalFileByIdAsync(int id)
        {
            return await _context.Medicalfiles.FindAsync(id);
        }

        public async Task AddMedicalFileAsync(Medicalfile medicalFile)
        {
            _context.Medicalfiles.Add(medicalFile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicalFileAsync(Medicalfile medicalFile)
        {
            _context.Medicalfiles.Update(medicalFile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicalFileAsync(int id)
        {
            var medicalFile = await _context.Medicalfiles.FindAsync(id);
            if (medicalFile != null)
            {
                _context.Medicalfiles.Remove(medicalFile);
                await _context.SaveChangesAsync();
            }
        }
    }
}
