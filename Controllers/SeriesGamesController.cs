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
    public class SeriesGamesController : Controller
    {
        private readonly DBStalkerContext _context;

        public SeriesGamesController(DBStalkerContext context)
        {
            _context = context;
        }

        // GET: SeriesGames
        public async Task<IActionResult> Index()
        {
            return View(await _context.SeriesGame.ToListAsync());
        }

        // GET: SeriesGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seriesGame = await _context.SeriesGame
                .FirstOrDefaultAsync(m => m.SgId == id);
            if (seriesGame == null)
            {
                return NotFound();
            }

            //return View(seriesGame);
            return RedirectToAction("Index", "GroupSgs", new { id = seriesGame.SgId, name = seriesGame.SgName });
        }

        // GET: SeriesGames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SeriesGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SgId,SgName")] SeriesGame seriesGame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seriesGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seriesGame);
        }

        // GET: SeriesGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seriesGame = await _context.SeriesGame.FindAsync(id);
            if (seriesGame == null)
            {
                return NotFound();
            }
            return View(seriesGame);
        }

        // POST: SeriesGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SgId,SgName")] SeriesGame seriesGame)
        {
            if (id != seriesGame.SgId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seriesGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeriesGameExists(seriesGame.SgId))
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
            return View(seriesGame);
        }

        // GET: SeriesGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seriesGame = await _context.SeriesGame
                .FirstOrDefaultAsync(m => m.SgId == id);
            if (seriesGame == null)
            {
                return NotFound();
            }

            return View(seriesGame);
        }

        // POST: SeriesGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seriesGame = await _context.SeriesGame.FindAsync(id);
            _context.SeriesGame.Remove(seriesGame);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeriesGameExists(int id)
        {
            return _context.SeriesGame.Any(e => e.SgId == id);
        }
    }
}
