using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StalkerApplication;

namespace StalkerApplication.Controllers
{
    public class TypeEquipmentsController : Controller
    {
        private readonly DBStalkerContext _context;

        public TypeEquipmentsController(DBStalkerContext context)
        {
            _context = context;
        }

        // GET: TypeEquipments
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeEquipment.ToListAsync());
        }

        // GET: TypeEquipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeEquipment = await _context.TypeEquipment
                .FirstOrDefaultAsync(m => m.TeId == id);
            if (typeEquipment == null)
            {
                return NotFound();
            }

            //return View(typeEquipment);
            return RedirectToAction("Index", "Equipments", new { id = typeEquipment.TeId, name = typeEquipment.TeName });
        }

        // GET: TypeEquipments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeEquipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeId,TeName")] TypeEquipment typeEquipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeEquipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeEquipment);
        }

        // GET: TypeEquipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeEquipment = await _context.TypeEquipment.FindAsync(id);
            if (typeEquipment == null)
            {
                return NotFound();
            }
            return View(typeEquipment);
        }

        // POST: TypeEquipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeId,TeName")] TypeEquipment typeEquipment)
        {
            if (id != typeEquipment.TeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeEquipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeEquipmentExists(typeEquipment.TeId))
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
            return View(typeEquipment);
        }

        // GET: TypeEquipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeEquipment = await _context.TypeEquipment
                .FirstOrDefaultAsync(m => m.TeId == id);
            if (typeEquipment == null)
            {
                return NotFound();
            }

            return View(typeEquipment);
        }

        // POST: TypeEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeEquipment = await _context.TypeEquipment.FindAsync(id);
            _context.TypeEquipment.Remove(typeEquipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeEquipmentExists(int id)
        {
            return _context.TypeEquipment.Any(e => e.TeId == id);
        }
    }
}
