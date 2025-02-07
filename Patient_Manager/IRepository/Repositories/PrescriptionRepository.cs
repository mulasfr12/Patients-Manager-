using Microsoft.EntityFrameworkCore;
using Patient_Manager.Data;
using Patient_Manager.Models;

namespace Patient_Manager.IRepository.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
        {
            return await _context.Prescriptions.ToListAsync();
        }

        public async Task<Prescription> GetPrescriptionByIdAsync(int id)
        {
            return await _context.Prescriptions.FindAsync(id);
        }

        public async Task AddPrescriptionAsync(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePrescriptionAsync(Prescription prescription)
        {
            _context.Prescriptions.Update(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePrescriptionAsync(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
                await _context.SaveChangesAsync();
            }
        }
    }
}
