﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Garage2._0.Data;
using Garage2._0.Models.Entities;
using Garage2._0.Models.ViewModels;

namespace Garage2._0.Controllers
{
    public class ParkeratFordonController : Controller
    {
        private readonly Garage2_0Context _context;
        private const int timPris = 60;
        private const int capacity = 20;
        private decimal[] garage = new decimal[capacity];

        public ParkeratFordonController(Garage2_0Context context)
        {
            _context = context;
            InitGarage();
        }

        // GET: ParkeratFordons
        public async Task<IActionResult> Index()
        {
            var fordon = _context.ParkeratFordon.Select(f => new FordonOversiktViewModel
            {
                Id = f.Id,
                FordonsTyp = f.FordonsTyp,
                RegNr = f.RegNr,
                AnkomstTid = f.AnkomstTid
            });

            return View(await fordon.ToListAsync());
        }

        private void InitGarage()
        {
            var fordon = _context.ParkeratFordon.ToList();
            var sistaFordon = fordon.Select(f => f.ParkeringsIndex).Max();
            int index = 0;

            for (int i = 0; i < sistaFordon + 1; i++)
            {
                switch (fordon[index].FordonsTyp)
                {
                    case FordonsTyp.Flygplan:
                    case FordonsTyp.Bat:
                        garage[fordon[index].ParkeringsIndex] = 1m;
                        garage[fordon[index].ParkeringsIndex + 1] = 1m;
                        garage[fordon[index].ParkeringsIndex + 2] = 1m;
                        i += 2;
                        break;
                    case FordonsTyp.Bil:
                        garage[fordon[index].ParkeringsIndex] = 1m;
                        break;
                    case FordonsTyp.Motorcykel:
                        garage[fordon[index].ParkeringsIndex] = 1/3m;
                        break;
                    case FordonsTyp.Buss:
                        garage[fordon[index].ParkeringsIndex] = 1m;
                        garage[fordon[index].ParkeringsIndex + 1] = 1m;
                        i += 1;
                        break;
                }
                index++;
            }
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
            if (RegNrExisterarValidering(fordonViewModel.RegNr, false))
            {
                ModelState.AddModelError("RegNr", "Registreringsnumret existerar redan.");
                return View(fordonViewModel);
            }

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
                TempData["OkFeedbackMsg"] = $"Parkerat fordon med reg nr {fordonViewModel.RegNr}";
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

            if (RegNrExisterarValidering(parkeratFordonViewModel.RegNr, true))
            {
                ModelState.AddModelError("RegNr", "Registreringsnumret existerar redan.");
                return View(parkeratFordonViewModel);
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

                    await _context.SaveChangesAsync();
                    TempData["OkFeedbackMsg"] = $"Uppdaterat fordon med reg nr {parkeratFordonViewModel.RegNr}";
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
                 TempData["OkFeedbackMsg"] = $"Hämtar fordon med reg nr {parkeratFordon.RegNr}";

                ////Kvitto?


                ////Ja 
                //var model = Kvitto(parkeratFordon);
                ////skicka till kvittovy
                //return View("Kvitto", model);
            }
            


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Kvitto(int? id)
        {
            if (id == null) { return NotFound(); }
            var parkeratFordon = await _context.ParkeratFordon
               .FirstOrDefaultAsync(m => m.Id == id);
            _context.ParkeratFordon.Remove(parkeratFordon);
            await _context.SaveChangesAsync();
            TempData["OkFeedbackMsg"] = $"Hämtat fordon med reg nr {parkeratFordon.RegNr} samt kvitto.";

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

            return View(model);

        }
            //private KvittoViewModel Kvitto(ParkeratFordon parkeratFordon)
            //{
            //    DateTime utcheckTid = DateTime.Now;
            //    TimeSpan tid = RaknaUtTid(parkeratFordon.AnkomstTid, utcheckTid);
            //    int totalPris = RaknaUtPris(timPris, tid);

            //    var model = new KvittoViewModel
            //    {
            //        RegNr = parkeratFordon.RegNr,
            //        AnkomstTid = parkeratFordon.AnkomstTid,
            //        UtchecksTid = utcheckTid,
            //        Pris = timPris,
            //        TotalPris = totalPris

            //    };

            //    return model;
            //}

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

            var result = await model.Select(f => new FordonOversiktViewModel
            {
                Id = f.Id,
                FordonsTyp = f.FordonsTyp,
                RegNr = f.RegNr,
                AnkomstTid = f.AnkomstTid

            }).ToListAsync();

            return View(nameof(Index), result);
        }

        private bool RegNrExisterarValidering(string regNr, bool editerar)
        {
            var fordonReg = _context.ParkeratFordon.FirstOrDefault(v => v.RegNr == regNr);
            if(editerar)
            {
                if (fordonReg == null || fordonReg.RegNr == regNr)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (fordonReg == null)
                {
                    return false;
                }
                return true;
            }
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
                timeCalculator += RaknaUtTid(item.AnkomstTid, DateTime.Now).TotalMinutes;

            }
            result.Intäkter = timeCalculator * 2;
            result.GenomsnittligParkeradTid = timeCalculator / divider;
            result.AntalBatar = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Bat)).Count();
            result.AntalBilar = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Bil)).Count();
            result.AntalBussar = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Buss)).Count();
            result.AntalFlygplan = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Flygplan)).Count();
            result.AntalMotorcyklar = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Motorcykel)).Count();
            return View(result);
        }
    }
}