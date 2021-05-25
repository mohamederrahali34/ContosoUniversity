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
    public class CoordonateurController : Controller
    {
        private readonly SchoolContext _context;

        public CoordonateurController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Coordonateur
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Stages.Include(s => s.Encadrant).Include(s =>s.Stagiaire).Include(s=>s.Binome);
            return View(await schoolContext.ToListAsync());
        }
        public async Task<IActionResult> Valider(int ?id)
        {


            var stage = _context.Stages.Single(s => s.StageID == id);
            var stageID = _context.Stages.Single(s => s.StageID == id).StageID;
            var newstage = new Stage
            {
                StageID = stage.StageID,
                Binome = stage.Binome,
                Description = stage.Description,
                SignatureValidation =1,
                DateDebut = stage.DateDebut,
                Encadrant = stage.Encadrant,
                Pays = stage.Pays,
                DateFin = stage.DateFin,
                EnseignantID = stage.EnseignantID,
                OrganismeAceuil = stage.OrganismeAceuil,
                Stagiaire = stage.Stagiaire,
                Ville = stage.Ville
            };
           
        
                try
                {
                _context.Stages.Remove(stage);
                    _context.Add(newstage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StageExists(stage.StageID))
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

        public async Task<IActionResult> Refuser(int? id)
        {
            var stage = _context.Stages.Single(s => s.StageID == id);
            var stageID = _context.Stages.Single(s => s.StageID == id).StageID;
            var newstage = new Stage
            {
                StageID = stage.StageID,
                Binome = stage.Binome,
                Description = stage.Description,
                SignatureValidation = 0,
                DateDebut = stage.DateDebut,
                Encadrant = stage.Encadrant,
                Pays = stage.Pays,
                DateFin = stage.DateFin,
                EnseignantID = stage.EnseignantID,
                OrganismeAceuil = stage.OrganismeAceuil,
                Stagiaire = stage.Stagiaire,
                Ville = stage.Ville
            };


            try
            {
                _context.Stages.Remove(stage);
                _context.Add(newstage);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StageExists(stage.StageID))
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
        // GET: Coordonateur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages
                .Include(s => s.Encadrant)
                .FirstOrDefaultAsync(m => m.StageID == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }

        // GET: Coordonateur/Create
        public IActionResult Create()
        {
            ViewData["EnseignantID"] = new SelectList(_context.Enseignants, "EnseignantID", "Discriminator");
            return View();
        }

        // POST: Coordonateur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StageID,Description,OrganismeAceuil,Pays,Ville,SignatureValidation,DateDebut,DateFin,EnseignantID")] Stage stage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnseignantID"] = new SelectList(_context.Enseignants, "EnseignantID", "Discriminator", stage.EnseignantID);
            return View(stage);
        }

        // GET: Coordonateur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages.FindAsync(id);
            if (stage == null)
            {
                return NotFound();
            }
            ViewData["EnseignantID"] = new SelectList(_context.Enseignants, "EnseignantID", "Discriminator", stage.EnseignantID);
            return View(stage);
        }

        // POST: Coordonateur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StageID,Description,OrganismeAceuil,Pays,Ville,SignatureValidation,DateDebut,DateFin,EnseignantID")] Stage stage)
        {
            if (id != stage.StageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StageExists(stage.StageID))
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
            ViewData["EnseignantID"] = new SelectList(_context.Enseignants, "EnseignantID", "Discriminator", stage.EnseignantID);
            return View(stage);
        }

        // GET: Coordonateur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages
                .Include(s => s.Encadrant)
                .FirstOrDefaultAsync(m => m.StageID == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }

        // POST: Coordonateur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stage = await _context.Stages.FindAsync(id);
            _context.Stages.Remove(stage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StageExists(int id)
        {
            return _context.Stages.Any(e => e.StageID == id);
        }
    }
}
