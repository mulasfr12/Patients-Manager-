namespace Patient_Manager.IRepository.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using global::Patient_Manager.Data;
    using global::Patient_Manager.Models;

    namespace Patient_Manager.Repository
    {
        public class MedicalRecordRepository : IMedicalRecordRepository
        {
            private readonly ApplicationDbContext _context;

            public MedicalRecordRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Medicalrecord>> GetAllMedicalRecordsAsync()
            {
                return await _context.Medicalrecords.ToListAsync();
            }

            public async Task<Medicalrecord> GetMedicalRecordByIdAsync(int id)
            {
                return await _context.Medicalrecords.FindAsync(id);
            }

            public async Task AddMedicalRecordAsync(Medicalrecord medicalRecord)
            {
                _context.Medicalrecords.Add(medicalRecord);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateMedicalRecordAsync(Medicalrecord medicalRecord)
            {
                _context.Medicalrecords.Update(medicalRecord);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteMedicalRecordAsync(int id)
            {
                var medicalRecord = await _context.Medicalrecords.FindAsync(id);
                if (medicalRecord != null)
                {
                    _context.Medicalrecords.Remove(medicalRecord);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }

}
