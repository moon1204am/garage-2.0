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
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Garage2._0.Controllers
{
    public class ParkeratFordonController : Controller
    {
        private readonly Garage2_0Context _context;

        private const int timPris = 60;
        private ParkeratFordon[] GarageArray = new ParkeratFordon[150];
        private int antalParkeradeFordon = 0;

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
                var fordonReg = _context.ParkeratFordon.FirstOrDefault(v => v.RegNr == fordonViewModel.RegNr);
                if (fordonReg != null)
                {
                    ModelState.AddModelError("RegNr", "Registreringsnumret existerar redan.");
                    return View(fordonViewModel);
                }
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
                TempData["OkParkeraMsg"] = $"Parkerat fordon med reg nr {fordonViewModel.RegNr}";
                return RedirectToAction(nameof(Index));
            }
            return View(fordonViewModel);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult RegNrExisterar(string regNr)
        {
            var fordon = _context.ParkeratFordon.FirstOrDefault(v => v.RegNr == regNr);
            if (fordon == null || fordon.RegNr == regNr)
            {
                return Json(true);
            }
            return Json($"Registreringsnumret existerar redan.");
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
            var fordonViewModel = new FordonViewModel
            {
                RegNr = parkeratFordon.RegNr,
                AntalHjul = parkeratFordon.AntalHjul,
                Modell = parkeratFordon.Modell,
                FordonsTyp = parkeratFordon.FordonsTyp,
                Farg = parkeratFordon.Farg,
                Marke = parkeratFordon.Marke
            };

            return View(fordonViewModel);
        }

        // POST: ParkeratFordons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FordonViewModel parkeratFordonViewModel)
        {
            if (id != parkeratFordonViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               

                try
                {
                    var parkeratFordon = await _context.ParkeratFordon.FindAsync(id);
                    parkeratFordon.FordonsTyp = parkeratFordonViewModel.FordonsTyp;
                    parkeratFordon.RegNr = parkeratFordonViewModel.RegNr;
                    parkeratFordon.Farg = parkeratFordonViewModel.Farg;
                    parkeratFordon.Marke = parkeratFordonViewModel.Marke;
                    parkeratFordon.Modell = parkeratFordonViewModel.Modell;
                    parkeratFordon.AntalHjul = parkeratFordonViewModel.AntalHjul;
                    _context.Update(parkeratFordon);

                  //  _context.Entry(parkeratFordon).Property(p => p.AnkomstTid).IsModified = false;
                    await _context.SaveChangesAsync();
                    TempData["OkParkeraMsg"] = $"Uppdaterat fordon med reg nr {parkeratFordonViewModel.RegNr}";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkeratFordonExists(parkeratFordonViewModel.Id))
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
            return View(parkeratFordonViewModel);

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
                 await _context.SaveChangesAsync();

                //Kvitto?


                //Ja 
                var model = Kvitto(parkeratFordon);
                //skicka till kvittovy
                return View("Kvitto", model);
            }
            


            return RedirectToAction(nameof(Index));
        }

        
        private KvittoViewModel Kvitto(ParkeratFordon parkeratFordon)
        {
            DateTime utcheckTid = DateTime.Now;
            TimeSpan tid = RaknaUtTid(parkeratFordon.AnkomstTid, utcheckTid);
            int totalPris = RaknaUtPris(timPris, tid);

            var model = new KvittoViewModel
            {
                RegNr = parkeratFordon.RegNr,
                AnkomstTid = parkeratFordon.AnkomstTid,
                UtchecksTid = utcheckTid,
                Pris = timPris,
                TotalPris = totalPris

            };

            return model;
        }
        
        private TimeSpan RaknaUtTid (DateTime ankomst, DateTime utckeck)
        {
            return utckeck.Subtract(ankomst);
            
        }

        private int RaknaUtPris(int pris, TimeSpan parkeringstid)
        {
            return parkeringstid.Minutes * pris /60;
            

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

        public async Task<IActionResult> Statistik()
        {
            var parkeradeFordon = _context.ParkeratFordon;          
            var result = new StatistikViewModel();
            int? count = 0;
            double timeCalculator = 0;
            double divider = parkeradeFordon.Count();
            
            foreach (var item in parkeradeFordon)
            {
                count += item.AntalHjul;
            }           
            result.AntalHjulIGaraget = count;

            foreach (var item in parkeradeFordon)
            {
                timeCalculator +=  RaknaUtTid(item.AnkomstTid, DateTime.Now).TotalMinutes;
                
            }
            result.Intäkter = timeCalculator * 2;
            result.GenomsnittligParkeradTid = timeCalculator / divider;
            result.AntalBatar = parkeradeFordon.Where(p=>p.FordonsTyp.Equals(FordonsTyp.Bat)).Count();
            result.AntalBilar = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Bil)).Count();
            result.AntalBussar = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Buss)).Count();
            result.AntalFlygplan = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Flygplan)).Count();
            result.AntalMotorcyklar = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Motorcykel)).Count();
            return View(result);
        }

        public IActionResult ParkeraArray(ParkeratFordon parkeratFordon)
        {


            switch (parkeratFordon.FordonsTyp)
            {

                case FordonsTyp.Flygplan:
                    ParkeraFlygplan(parkeratFordon);
                    break;
                case FordonsTyp.Motorcykel:
                    ParkeraMotorcykel(parkeratFordon);
                    break;
                case FordonsTyp.Buss:
                    ParkeraBuss(parkeratFordon);
                    break;
                case FordonsTyp.Bil:
                    ParkeraBil(parkeratFordon);
                    break;
                case FordonsTyp.Bat:
                    ParkeraBat(parkeratFordon);
                    break;
                    default;




                    //if (parkeratFordon.FordonsTyp == FordonsTyp.Flygplan)
                    //{

                    //}


            }
            return View(nameof(Index));

        }

        IActionResult ParkeraFlygplan(ParkeratFordon parkeratFordon)
        {       
                int i;
                for (i = 0; i < GarageArray.Length; i++)
            {
                if (GarageArray[i] == null && GarageArray[i + 1] == null && GarageArray[i + 2] == null)
                {
                    GarageArray[i] = parkeratFordon;
                    GarageArray[i + 1] = parkeratFordon;
                    GarageArray[i + 2] = parkeratFordon;
                }
                }
                antalParkeradeFordon += 3;
              //  parkeringsPlats = i;

                return View(nameof(Index));
        }


    }
}
