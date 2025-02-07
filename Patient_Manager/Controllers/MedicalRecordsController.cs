using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Patient_Manager.Data;
using Patient_Manager.Models;

namespace Patient_Manager.Controllers
{
    public class MedicalRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MedicalRecords
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Medicalrecords.Include(m => m.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MedicalRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalrecord = await _context.Medicalrecords
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.Recordid == id);
            if (medicalrecord == null)
            {
                return NotFound();
            }

            return View(medicalrecord);
        }

        // GET: MedicalRecords/Create
        public IActionResult Create()
        {
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid");
            return View();
        }

        // POST: MedicalRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Recordid,Patientid,Diseasename,Startdate,Enddate")] Medicalrecord medicalrecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalrecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", medicalrecord.Patientid);
            return View(medicalrecord);
        }

        // GET: MedicalRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalrecord = await _context.Medicalrecords.FindAsync(id);
            if (medicalrecord == null)
            {
                return NotFound();
            }
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", medicalrecord.Patientid);
            return View(medicalrecord);
        }

        // POST: MedicalRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Recordid,Patientid,Diseasename,Startdate,Enddate")] Medicalrecord medicalrecord)
        {
            if (id != medicalrecord.Recordid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalrecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalrecordExists(medicalrecord.Recordid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", medicalrecord.Patientid);
            return View(medicalrecord);
        }

        // GET: MedicalRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalrecord = await _context.Medicalrecords
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.Recordid == id);
            if (medicalrecord == null)
            {
                return NotFound();
            }

            return View(medicalrecord);
        }

        // POST: MedicalRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalrecord = await _context.Medicalrecords.FindAsync(id);
            if (medicalrecord != null)
            {
                _context.Medicalrecords.Remove(medicalrecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalrecordExists(int id)
        {
            return _context.Medicalrecords.Any(e => e.Recordid == id);
        }
    }
}
