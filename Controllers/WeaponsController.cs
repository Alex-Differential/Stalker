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
    public class WeaponsController : Controller
    {
        private readonly DBStalkerContext _context;

        public WeaponsController(DBStalkerContext context)
        {
            _context = context;
        }

        // GET: Weapons
        public async Task<IActionResult> Index(int? id, string ? name)
        {
            if (id == null) return RedirectToAction("TypeWeapons", "Index");
            ViewBag.TwID = id;
            ViewBag.TwName = name;
            var dBStalkerContext = _context.Weapons.Where(w => w.WpId == id).Include(w => w.WpTwNavigation);
            return View(await dBStalkerContext.ToListAsync());
        }

        // GET: Weapons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapons = await _context.Weapons
                .Include(w => w.WpPcNavigation)
                .Include(w => w.WpTwNavigation)
                .FirstOrDefaultAsync(m => m.WpId == id);
            if (weapons == null)
            {
                return NotFound();
            }

            return View(weapons);
        }

        // GET: Weapons/Create
        public IActionResult Create(int twId)
        {
            //ViewData["WpPc"] = new SelectList(_context.ProducingCountry, "PcId", "PcId");
            //ViewData["WpTw"] = new SelectList(_context.TypeWeapons, "TwId", "TwName");
            ViewBag.TwID = twId;
            ViewBag.TwName = _context.TypeWeapons.Where(c => c.TwId == twId).FirstOrDefault().TwName;
            return View();
        }

        // POST: Weapons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int twId, [Bind("WpId,WpName,WpTw,WpPc,WpWg,WpRn,WpMg,WpTf")] Weapons weapons)
        {
            weapons.WpTw = twId;
            if (ModelState.IsValid)
            {
                _context.Add(weapons);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Weapons", new { id = twId, name = _context.TypeWeapons.Where(c => c.TwId == twId).FirstOrDefault().TwName });
            }
            //ViewData["WpPc"] = new SelectList(_context.ProducingCountry, "PcId", "PcId", weapons.WpPc);
            //ViewData["WpTw"] = new SelectList(_context.TypeWeapons, "TwId", "TwName", weapons.WpTw);
            return RedirectToAction("Index", "Weapons", new { id = twId, name = _context.TypeWeapons.Where(c => c.TwId == twId).FirstOrDefault().TwName });

           // return View(weapons);
        }

        // GET: Weapons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapons = await _context.Weapons.FindAsync(id);
            if (weapons == null)
            {
                return NotFound();
            }
            ViewData["WpPc"] = new SelectList(_context.ProducingCountry, "PcId", "PcId", weapons.WpPc);
            ViewData["WpTw"] = new SelectList(_context.TypeWeapons, "TwId", "TwName", weapons.WpTw);
            return View(weapons);
        }

        // POST: Weapons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WpId,WpName,WpTw,WpPc,WpWg,WpRn,WpMg,WpTf")] Weapons weapons)
        {
            if (id != weapons.WpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weapons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeaponsExists(weapons.WpId))
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
            ViewData["WpPc"] = new SelectList(_context.ProducingCountry, "PcId", "PcId", weapons.WpPc);
            ViewData["WpTw"] = new SelectList(_context.TypeWeapons, "TwId", "TwName", weapons.WpTw);
            return View(weapons);
        }

        // GET: Weapons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapons = await _context.Weapons
                .Include(w => w.WpPcNavigation)
                .Include(w => w.WpTwNavigation)
                .FirstOrDefaultAsync(m => m.WpId == id);
            if (weapons == null)
            {
                return NotFound();
            }

            return View(weapons);
        }

        // POST: Weapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weapons = await _context.Weapons.FindAsync(id);
            _context.Weapons.Remove(weapons);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeaponsExists(int id)
        {
            return _context.Weapons.Any(e => e.WpId == id);
        }
    }
}
