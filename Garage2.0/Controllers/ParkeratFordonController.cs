﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2._0.Data;
using Garage2._0.Models.Entities;
using Garage2._0.Models.ViewModels;

namespace Garage2._0.Controllers
{
    public class ParkeratFordonController : Controller
    {
        private readonly Garage2_0Context _context;

        public ParkeratFordonController(Garage2_0Context context)
        {
            _context = context;
        }

        // GET: ParkeratFordons
        public async Task<IActionResult> Index()
        {
              return _context.ParkeratFordon != null ? 
                          View(await _context.ParkeratFordon.ToListAsync()) :
                          Problem("Entity set 'Garage2_0Context.ParkeratFordon'  is null.");


        }


        
        // GET: ParkeratFordons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParkeratFordon == null)
            {
                return NotFound();
            }

            var parkeratFordon = await _context.ParkeratFordon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkeratFordon == null)
            {
                return NotFound();
            }

            return View(parkeratFordon);
        }

        // GET: ParkeratFordons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkeratFordons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FordonViewModel fordonViewModel)
        {
            if (ModelState.IsValid)
            {
                var fordon = new ParkeratFordon
                {
                    FordonsTyp = fordonViewModel.FordonsTyp,
                    RegNr = fordonViewModel.RegNr,
                    Farg = fordonViewModel.Farg,
                    Marke = fordonViewModel.Marke,
                    Modell = fordonViewModel.Modell,
                    AntalHjul = fordonViewModel?.AntalHjul,
                    AnkomstTid = DateTime.Now
                };
                _context.Add(fordon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fordonViewModel);
        }

        // GET: ParkeratFordons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParkeratFordon == null)
            {
                return NotFound();
            }

            var parkeratFordon = await _context.ParkeratFordon.FindAsync(id);
            if (parkeratFordon == null)
            {
                return NotFound();
            }
            return View(parkeratFordon);
        }

        // POST: ParkeratFordons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ParkeratFordon parkeratFordon)
        {
            if (id != parkeratFordon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkeratFordon);

                    _context.Entry(parkeratFordon).Property(p => p.AnkomstTid).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkeratFordonExists(parkeratFordon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("EditMessage");
               // return RedirectToAction(nameof(Index));
            }
            return View(parkeratFordon);
        }

        // GET: ParkeratFordons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParkeratFordon == null)
            {
                return NotFound();
            }

            var parkeratFordon = await _context.ParkeratFordon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkeratFordon == null)
            {
                return NotFound();
            }

            return View(parkeratFordon);
        }

        
        // POST: ParkeratFordons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParkeratFordon == null)
            {
                return Problem("Entity set 'Garage2_0Context.ParkeratFordon'  is null.");
            }
            var parkeratFordon = await _context.ParkeratFordon.FindAsync(id);
            if (parkeratFordon != null)
            {
                _context.ParkeratFordon.Remove(parkeratFordon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        private async Task<IActionResult> Kvitto(int? id)
        {
            if (id == null || _context.ParkeratFordon == null)
            {
                 return NotFound();
            }
            var kvittoViewModel = await _context.ParkeratFordon
                .FirstOrDefaultAsync(m => m.Id == id);
            DateTime utcheckTid = DateTime.Now;
            if (kvittoViewModel == null)
            {
                return NotFound();
            }

            int pris = 50;
            TimeSpan tid = RaknaUtTid(kvittoViewModel.AnkomstTid, utcheckTid);
            double totalPris = RaknaUtPris(pris, tid);
            return View(kvittoViewModel);
        }
        
        private TimeSpan RaknaUtTid (DateTime ankomst, DateTime utckeck)
        {
            return utckeck.Subtract(ankomst);
            
        }

        private double RaknaUtPris(int pris, TimeSpan parkeringstid)
        {
            double totalPris = (parkeringstid.TotalMinutes * Convert.ToDouble(pris)) / 60;
            return Math.Round(totalPris, 2, MidpointRounding.AwayFromZero);

        }

        private bool ParkeratFordonExists(int id)
        {
          return (_context.ParkeratFordon?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Filter(string regnr)
        {
            var model = string.IsNullOrWhiteSpace(regnr) ?
                                                _context.ParkeratFordon :
                                                _context.ParkeratFordon.Where(m => m.RegNr.StartsWith(regnr));

           


            return View(nameof(Index), await model.ToListAsync());
        }

        public IActionResult EditMessage()
        {
            ViewBag.EditCompleteMessage = "Uppdateringen är slutförd";
            return View();
        }

    }


}
