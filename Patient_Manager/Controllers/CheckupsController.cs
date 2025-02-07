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
    public class CheckupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Checkups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Checkups.Include(c => c.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Checkups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkup = await _context.Checkups
                .Include(c => c.Patient)
                .FirstOrDefaultAsync(m => m.Checkupid == id);
            if (checkup == null)
            {
                return NotFound();
            }

            return View(checkup);
        }

        // GET: Checkups/Create
        public IActionResult Create()
        {
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid");
            return View();
        }

        // POST: Checkups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Checkupid,Patientid,Checkuptype,Checkupdate,Checkuptime")] Checkup checkup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", checkup.Patientid);
            return View(checkup);
        }

        // GET: Checkups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkup = await _context.Checkups.FindAsync(id);
            if (checkup == null)
            {
                return NotFound();
            }
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", checkup.Patientid);
            return View(checkup);
        }

        // POST: Checkups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Checkupid,Patientid,Checkuptype,Checkupdate,Checkuptime")] Checkup checkup)
        {
            if (id != checkup.Checkupid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckupExists(checkup.Checkupid))
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
            ViewData["Patientid"] = new SelectList(_context.Patients, "Patientid", "Patientid", checkup.Patientid);
            return View(checkup);
        }

        // GET: Checkups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkup = await _context.Checkups
                .Include(c => c.Patient)
                .FirstOrDefaultAsync(m => m.Checkupid == id);
            if (checkup == null)
            {
                return NotFound();
            }

            return View(checkup);
        }

        // POST: Checkups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkup = await _context.Checkups.FindAsync(id);
            if (checkup != null)
            {
                _context.Checkups.Remove(checkup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckupExists(int id)
        {
            return _context.Checkups.Any(e => e.Checkupid == id);
        }
    }
}
