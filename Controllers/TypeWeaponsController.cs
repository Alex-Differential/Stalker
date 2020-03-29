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
    public class TypeWeaponsController : Controller
    {
        private readonly DBStalkerContext _context;

        public TypeWeaponsController(DBStalkerContext context)
        {
            _context = context;
        }

        // GET: TypeWeapons
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeWeapons.ToListAsync());
        }

        // GET: TypeWeapons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeWeapons = await _context.TypeWeapons
                .FirstOrDefaultAsync(m => m.TwId == id);
            if (typeWeapons == null)
            {
                return NotFound();
            }

            //return View(typeWeapons);
            return RedirectToAction("Index", "Weapons", new { id = typeWeapons.TwId, name = typeWeapons.TwName});
        }

        // GET: TypeWeapons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeWeapons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TwId,TwName")] TypeWeapons typeWeapons)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeWeapons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeWeapons);
        }

        // GET: TypeWeapons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeWeapons = await _context.TypeWeapons.FindAsync(id);
            if (typeWeapons == null)
            {
                return NotFound();
            }
            return View(typeWeapons);
        }

        // POST: TypeWeapons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TwId,TwName")] TypeWeapons typeWeapons)
        {
            if (id != typeWeapons.TwId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeWeapons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeWeaponsExists(typeWeapons.TwId))
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
            return View(typeWeapons);
        }

        // GET: TypeWeapons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeWeapons = await _context.TypeWeapons
                .FirstOrDefaultAsync(m => m.TwId == id);
            if (typeWeapons == null)
            {
                return NotFound();
            }

            return View(typeWeapons);
        }

        // POST: TypeWeapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeWeapons = await _context.TypeWeapons.FindAsync(id);
            _context.TypeWeapons.Remove(typeWeapons);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeWeaponsExists(int id)
        {
            return _context.TypeWeapons.Any(e => e.TwId == id);
        }
    }
}
