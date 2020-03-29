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
    public class GroupingsController : Controller
    {
        private readonly DBStalkerContext _context;

        public GroupingsController(DBStalkerContext context)
        {
            _context = context;
        }

        // GET: Groupings
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Equipments", "Index");
            ViewBag.GrEq = id;
            ViewBag.EqName = name;
            var dBStalkerContext = _context.Grouping.Where(g => g.GrEq == id).Include(g => g.GrEqNavigation);
            return View(await dBStalkerContext.ToListAsync());
        }

        // GET: Groupings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grouping = await _context.Grouping
                .Include(g => g.GrEqNavigation)
                .FirstOrDefaultAsync(m => m.GrId == id);
            if (grouping == null)
            {
                return NotFound();
            }

            return View(grouping);
            //return RedirectToAction("Index", "GroupSgs", new { id = grouping.GrId, name = grouping.GrName });
        }

        // GET: Groupings/Create
        public IActionResult Create()
        {
            ViewData["GrEq"] = new SelectList(_context.Equipment, "EqId", "EqName");
            return View();
        }

        // POST: Groupings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GrId,GrName,GrEq,GrSg,GrInfo")] Grouping grouping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grouping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GrEq"] = new SelectList(_context.Equipment, "EqId", "EqName", grouping.GrEq);
            return View(grouping);
        }

        // GET: Groupings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grouping = await _context.Grouping.FindAsync(id);
            if (grouping == null)
            {
                return NotFound();
            }
            ViewData["GrEq"] = new SelectList(_context.Equipment, "EqId", "EqName", grouping.GrEq);
            return View(grouping);
        }

        // POST: Groupings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GrId,GrName,GrEq,GrSg,GrInfo")] Grouping grouping)
        {
            if (id != grouping.GrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grouping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupingExists(grouping.GrId))
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
            ViewData["GrEq"] = new SelectList(_context.Equipment, "EqId", "EqName", grouping.GrEq);
            return View(grouping);
        }

        // GET: Groupings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grouping = await _context.Grouping
                .Include(g => g.GrEqNavigation)
                .FirstOrDefaultAsync(m => m.GrId == id);
            if (grouping == null)
            {
                return NotFound();
            }

            return View(grouping);
        }

        // POST: Groupings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grouping = await _context.Grouping.FindAsync(id);
            _context.Grouping.Remove(grouping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupingExists(int id)
        {
            return _context.Grouping.Any(e => e.GrId == id);
        }
    }
}
