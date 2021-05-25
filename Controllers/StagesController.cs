using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Controllers
{
    public class StagesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly SchoolContext _context;
        public List<SoummissionData> soummissionsData;
        public StagesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Stages
        public async Task<IActionResult> Index()
        {

            var stages = _context.Stages.Include(s => s.Encadrant).
              Include(s => s.Binome).Include(s=>s.Stagiaire).AsNoTracking();
          if (stages.Count() >= 1)
            {
                ViewBag.hasSoummission = true;
            }
            else { 
                ViewBag.hasSoummission = false; 
            }
            return View(await stages.ToListAsync());
        }

        // GET: Stages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages
                .FirstOrDefaultAsync(m => m.StageID == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }

        // GET: Stages/Create
        public IActionResult Create()
        {
            PopulateEnseignantsDropDownList();
            PopulateStudentsDropDownList();
            return View();
        }
       
        // POST: Stages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StageID,Description,OrganismeAceuil,Pays,Ville,SignatureValidation,DateDebut,DateFin,StagiaireID,BinomeID,EnseignantID")] Stage stage)
        {
          
            ViewData["EncadrantID"] = stage.EnseignantID;
            if (ModelState.IsValid)
            {
                _context.Add(stage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateEnseignantsDropDownList();
            PopulateStudentsDropDownList();
            return View(stage);
        }
    
        // GET: Stages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var stage1 = _context.Stages.Single(s => s.StageID == id);
            var encadrant = _context.Enseignants.Single(e => e.EnseignantID==stage1.EnseignantID);
            PopulateEnseignantsDropDownList(encadrant.EnseignantID);

        var binomeID = _context.Stages.Include(x => x.Binome).Select(x=>x.Binome.ID);
            PopulateStudentsDropDownList(binomeID);
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages.FindAsync(id);
            if (stage == null)
            {
                return NotFound();
            }
            return View(stage);
        }

        // POST: Stages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            return View(stage);
        }

        // GET: Stages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages
                .FirstOrDefaultAsync(m => m.StageID == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }
        private  void PopulateEnseignantsDropDownList(object selectedEnseignant = null)
        {
            var enseignantsQuery = from e in _context.Enseignants
                                   orderby e.Nom
                                   select e;
            ViewBag.Enseignants = new SelectList(enseignantsQuery.AsNoTracking(), "EnseignantID", "FullName", selectedEnseignant); 
        }
        private void PopulateStudentsDropDownList(object selectedStudent = null)
        {
            var studentsQuery = from s in _context.Students
                                   orderby s.LastName
                                   select s;
            ViewBag.Students = new SelectList(studentsQuery.AsNoTracking(), "ID", "LastName", selectedStudent);
        }
        // POST: Stages/Delete/5
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
