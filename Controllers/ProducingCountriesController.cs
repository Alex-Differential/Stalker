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
    public class ProducingCountriesController : Controller
    {
        private readonly DBStalkerContext _context;

        public ProducingCountriesController(DBStalkerContext context)
        {
            _context = context;
        }

        // GET: ProducingCountries
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProducingCountry.ToListAsync());
        }

        // GET: ProducingCountries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producingCountry = await _context.ProducingCountry
                .FirstOrDefaultAsync(m => m.PcId == id);
            if (producingCountry == null)
            {
                return NotFound();
            }

            //return View(producingCountry);
            return RedirectToAction("Index", "Weapons", new { id = producingCountry.PcId, name = producingCountry.PcName });
        }

        // GET: ProducingCountries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProducingCountries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PcId,PcName")] ProducingCountry producingCountry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producingCountry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producingCountry);
        }

        // GET: ProducingCountries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producingCountry = await _context.ProducingCountry.FindAsync(id);
            if (producingCountry == null)
            {
                return NotFound();
            }
            return View(producingCountry);
        }

        // POST: ProducingCountries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PcId,PcName")] ProducingCountry producingCountry)
        {
            if (id != producingCountry.PcId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producingCountry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducingCountryExists(producingCountry.PcId))
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
            return View(producingCountry);
        }

        // GET: ProducingCountries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producingCountry = await _context.ProducingCountry
                .FirstOrDefaultAsync(m => m.PcId == id);
            if (producingCountry == null)
            {
                return NotFound();
            }

            return View(producingCountry);
        }

        // POST: ProducingCountries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producingCountry = await _context.ProducingCountry.FindAsync(id);
            _context.ProducingCountry.Remove(producingCountry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducingCountryExists(int id)
        {
            return _context.ProducingCountry.Any(e => e.PcId == id);
        }
    }
}
