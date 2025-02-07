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
    public class MedicalFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicalFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MedicalFiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Medicalfiles.Include(m => m.Checkup);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MedicalFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalfile = await _context.Medicalfiles
                .Include(m => m.Checkup)
                .FirstOrDefaultAsync(m => m.Fileid == id);
            if (medicalfile == null)
            {
                return NotFound();
            }

            return View(medicalfile);
        }

        // GET: MedicalFiles/Create
        public IActionResult Create()
        {
            ViewData["Checkupid"] = new SelectList(_context.Checkups, "Checkupid", "Checkupid");
            return View();
        }

        // POST: MedicalFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Fileid,Checkupid,Filepath,Uploadedat")] Medicalfile medicalfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Checkupid"] = new SelectList(_context.Checkups, "Checkupid", "Checkupid", medicalfile.Checkupid);
            return View(medicalfile);
        }

        // GET: MedicalFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalfile = await _context.Medicalfiles.FindAsync(id);
            if (medicalfile == null)
            {
                return NotFound();
            }
            ViewData["Checkupid"] = new SelectList(_context.Checkups, "Checkupid", "Checkupid", medicalfile.Checkupid);
            return View(medicalfile);
        }

        // POST: MedicalFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Fileid,Checkupid,Filepath,Uploadedat")] Medicalfile medicalfile)
        {
            if (id != medicalfile.Fileid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalfileExists(medicalfile.Fileid))
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
            ViewData["Checkupid"] = new SelectList(_context.Checkups, "Checkupid", "Checkupid", medicalfile.Checkupid);
            return View(medicalfile);
        }

        // GET: MedicalFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalfile = await _context.Medicalfiles
                .Include(m => m.Checkup)
                .FirstOrDefaultAsync(m => m.Fileid == id);
            if (medicalfile == null)
            {
                return NotFound();
            }

            return View(medicalfile);
        }

        // POST: MedicalFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalfile = await _context.Medicalfiles.FindAsync(id);
            if (medicalfile != null)
            {
                _context.Medicalfiles.Remove(medicalfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalfileExists(int id)
        {
            return _context.Medicalfiles.Any(e => e.Fileid == id);
        }
    }
}
