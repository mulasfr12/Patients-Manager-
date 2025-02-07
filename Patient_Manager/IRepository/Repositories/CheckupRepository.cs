using Microsoft.EntityFrameworkCore;
using Patient_Manager.Data;
using Patient_Manager.Models;

namespace Patient_Manager.IRepository.Repositories
{
    public class CheckupRepository : ICheckupRepository
    {
        private readonly ApplicationDbContext _context;

        public CheckupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Checkup>> GetAllCheckupsAsync()
        {
            return await _context.Checkups.ToListAsync();
        }

        public async Task<Checkup> GetCheckupByIdAsync(int id)
        {
            return await _context.Checkups.FindAsync(id);
        }

        public async Task AddCheckupAsync(Checkup checkup)
        {
            _context.Checkups.Add(checkup);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCheckupAsync(Checkup checkup)
        {
            _context.Checkups.Update(checkup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCheckupAsync(int id)
        {
            var checkup = await _context.Checkups.FindAsync(id);
            if (checkup != null)
            {
                _context.Checkups.Remove(checkup);
                await _context.SaveChangesAsync();
            }
        }
    }
}
