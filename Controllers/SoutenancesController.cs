using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class SoutenancesController : Controller
    {
        private readonly SchoolContext _context;

        public SoutenancesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Soutenances
        public async Task<IActionResult> Index()
        {
            var soutenances = _context.Soutenances.Include(s=> s.Stage) ;
            return View(await soutenances.ToListAsync());
        }

        // GET: Soutenances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soutenance = await _context.Soutenances
                .FirstOrDefaultAsync(m => m.SoutenanceID == id);
            if (soutenance == null)
            {
                return NotFound();
            }

            return View(soutenance);
        }

        // GET: Soutenances/Create
        public IActionResult Create()
        {
            PopulateStagesDropDownList();
            return View();
        }

        // POST: Soutenances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoutenanceID,Note,Jour,HeureDebut,HeureFin,Etat,Salle")] Soutenance soutenance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soutenance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(soutenance);
        }

        // GET: Soutenances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soutenance = await _context.Soutenances.FindAsync(id);
            if (soutenance == null)
            {
                return NotFound();
            }
            PopulateStagesDropDownList();
            return View(soutenance);
        }

        // POST: Soutenances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoutenanceID,Note,Jour,HeureDebut,HeureFin,Etat,Salle")] Soutenance soutenance)
        {
            if (id != soutenance.SoutenanceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soutenance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoutenanceExists(soutenance.SoutenanceID))
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
            return View(soutenance);
        }
        // GET: Soutenances/Noter/5
        public async Task<IActionResult> Noter(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soutenance = await _context.Soutenances.FindAsync(id);
            if (soutenance == null)
            {
                return NotFound();
            }
            PopulateStagesDropDownList();
            return View(soutenance);
        }

        // POST: Soutenances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Noter(int id, [Bind("SoutenanceID,Note,Jour,HeureDebut,HeureFin,Etat,Salle")] Soutenance soutenance)
        {
            if (id != soutenance.SoutenanceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soutenance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoutenanceExists(soutenance.SoutenanceID))
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
            return View(soutenance);
        }


        // GET: Soutenances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soutenance = await _context.Soutenances
                .FirstOrDefaultAsync(m => m.SoutenanceID == id);
            if (soutenance == null)
            {
                return NotFound();
            }

            return View(soutenance);
        }
       
        // POST: Soutenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soutenance = await _context.Soutenances.FindAsync(id);
            _context.Soutenances.Remove(soutenance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private void PopulateStagesDropDownList(object selectedStage = null)
        {
            var stagesQuery = from s in _context.Stages
                              orderby s.Description
                              select s;
            ViewBag.Stages = new SelectList(stagesQuery.AsNoTracking(), "StageID", "Description", selectedStage);
        }
        private bool SoutenanceExists(int id)
        {
            return _context.Soutenances.Any(e => e.SoutenanceID == id);
        }
    }
}
