using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StalkerApplication.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StalkerApplication.Controllers
{
    public class EquipmentsController : Controller
    {
        private readonly DBStalkerContext _context;

        public EquipmentsController(DBStalkerContext context)
        {
            _context = context;
        }

        // GET: Equipments
        public async Task<IActionResult> Index(int ? id, string ? name)
        {
            if (id == null) return RedirectToAction("TypeEquipments", "Index");
            ViewBag.EqTe = id;
            ViewBag.Name = name;
            var equipmentsbytype = _context.Equipment.Where(e => e.EqTe == id).Include(e => e.EqTeNavigation);
            return View(await equipmentsbytype.ToListAsync());
        }

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment
                .Include(e => e.EqTeNavigation)
                .FirstOrDefaultAsync(m => m.EqId == id);
            if (equipment == null)
            {
                return NotFound();
            }

            //return View(equipment);
            return RedirectToAction("Index", "Groupings", new { id = equipment.EqId, name = equipment.EqName });
        }

        // GET: Equipments/Create
        public IActionResult Create(int teid)
        {
            ViewBag.EqTe = teid;
            ViewBag.EqName = _context.TypeEquipment.Where(c => c.TeId == teid).FirstOrDefault().TeName;
            //ViewData["EqTe"] = new SelectList(_context.TypeEquipment, "TeId", "TeName");
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int teid, [Bind("EqId,EqName,EqTe")] Equipment equipment)
        {
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            //ViewData["EqTe"] = new SelectList(_context.TypeEquipment, "TeId", "TeName", equipment.EqTe);
            teid = 1;
            equipment.EqTe = teid; 
            if (ModelState.IsValid)
            {
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "Equipments", new { id = teid, name = _context.TypeEquipment.Where(e => e.TeId == teid).FirstOrDefault().TeName });
            }
            //ViewData["EqTe"] = new SelectList(_context.TypeEquipment, "TeId", "TeName", equipment.EqTe);
            //return View(equipment);
            return RedirectToAction("Index", "Equipments", new { id = teid, name = _context.TypeEquipment.Where(e => e.TeId == teid).FirstOrDefault().TeName });
        }

        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }
            ViewData["EqTe"] = new SelectList(_context.TypeEquipment, "TeId", "TeName", equipment.EqTe);
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EqId,EqName,EqTe")] Equipment equipment)
        {
            if (id != equipment.EqId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.EqId))
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
            ViewData["EqTe"] = new SelectList(_context.TypeEquipment, "TeId", "TeName", equipment.EqTe);
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment
                .Include(e => e.EqTeNavigation)
                .FirstOrDefaultAsync(m => m.EqId == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipment.Any(e => e.EqId == id);
        }
    }
}
