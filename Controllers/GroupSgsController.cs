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
    public class GroupSgsController : Controller
    {
        private readonly DBStalkerContext _context;

        public GroupSgsController(DBStalkerContext context)
        {
            _context = context;
        }

        // GET: GroupSgs
        public async Task<IActionResult> Index()
        {
            var dBStalkerContext = _context.GroupSg.Include(g => g.GsGr).Include(g => g.GsSg);
            return View(await dBStalkerContext.ToListAsync());
        }

        // GET: GroupSgs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupSg = await _context.GroupSg
                .Include(g => g.GsGr)
                .Include(g => g.GsSg)
                .FirstOrDefaultAsync(m => m.GsGrid == id);
            if (groupSg == null)
            {
                return NotFound();
            }

            return View(groupSg);
        }

        // GET: GroupSgs/Create
        public IActionResult Create()
        {
            ViewData["GsGrid"] = new SelectList(_context.Grouping, "GrId", "GrName");
            ViewData["GsSgid"] = new SelectList(_context.SeriesGame, "SgId", "SgName");
            return View();
        }

        // POST: GroupSgs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GsGrid,GsSgid")] GroupSg groupSg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupSg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GsGrid"] = new SelectList(_context.Grouping, "GrId", "GrName", groupSg.GsGrid);
            ViewData["GsSgid"] = new SelectList(_context.SeriesGame, "SgId", "SgName", groupSg.GsSgid);
            return View(groupSg);
        }

        // GET: GroupSgs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupSg = await _context.GroupSg.FindAsync(id);
            if (groupSg == null)
            {
                return NotFound();
            }
            ViewData["GsGrid"] = new SelectList(_context.Grouping, "GrId", "GrName", groupSg.GsGrid);
            ViewData["GsSgid"] = new SelectList(_context.SeriesGame, "SgId", "SgName", groupSg.GsSgid);
            return View(groupSg);
        }

        // POST: GroupSgs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GsGrid,GsSgid")] GroupSg groupSg)
        {
            if (id != groupSg.GsGrid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupSg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupSgExists(groupSg.GsGrid))
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
            ViewData["GsGrid"] = new SelectList(_context.Grouping, "GrId", "GrName", groupSg.GsGrid);
            ViewData["GsSgid"] = new SelectList(_context.SeriesGame, "SgId", "SgName", groupSg.GsSgid);
            return View(groupSg);
        }

        // GET: GroupSgs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupSg = await _context.GroupSg
                .Include(g => g.GsGr)
                .Include(g => g.GsSg)
                .FirstOrDefaultAsync(m => m.GsGrid == id);
            if (groupSg == null)
            {
                return NotFound();
            }

            return View(groupSg);
        }

        // POST: GroupSgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupSg = await _context.GroupSg.FindAsync(id);
            _context.GroupSg.Remove(groupSg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupSgExists(int id)
        {
            return _context.GroupSg.Any(e => e.GsGrid == id);
        }
    }
}
