using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Garage2._0.Data;
using Garage2._0.Models.Entities;
using Garage2._0.Models.ViewModels;
using System.Text;

namespace Garage2._0.Controllers
{
    public class ParkeratFordonController : Controller
    {
        private readonly Garage2_0Context _context;
        private const int timPris = 60;
        private const int minutPris = 1;
        private const int capacity = 20;
        private double[] garage = new double[capacity];
        private double antal;

        public ParkeratFordonController(Garage2_0Context context)
        {
            _context = context;
            InitGarage();
        }

        public double AntalFordonIGaraget => antal;

        // GET: ParkeratFordons
        public async Task<IActionResult> Index()
        {
            var fordon = await _context.ParkeratFordon.Select(v => new FordonOversiktViewModel
            {
                Id = v.Id,
                FordonsTyp = v.FordonsTyp,
                RegNr = v.RegNr,
                AnkomstTid = v.AnkomstTid

            }).ToListAsync();

            var index = new StartsidaViewModel
            {
                ParkeradeFordon = fordon,
                AntalLedigaPlatser = capacity - RaknaLedigaPlatser()
            };

            return View(index);
        }

        public IActionResult GaragePlatser()
        {
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach(var item in garage)
            {
                
                if (item == 0d)
                    sb.AppendLine($"Plats {i}");
                else if (item == 1/3d) {
                    sb.AppendLine($"Plats {i}");
                    sb.AppendLine($"Två lediga motorcykelplatser");
                }
                else if(item == 2/3d)
                {
                    sb.AppendLine($"Plats {i}");
                    sb.AppendLine($"En ledig motorcykel plats");
                }
                i++;
            }
            var model = new GarageOversiktViewModel
            {
                LedigaPlatser = sb.ToString(),
                AntalLedigaPlatser = capacity - antal
            };
            return View(model);
        }

        private void InitGarage()
        {
            var fordon = _context.ParkeratFordon.ToList();
            int index = 0;
            foreach (var f in fordon)
            {
                switch (fordon[index].FordonsTyp)
                {
                    case FordonsTyp.Flygplan:
                    case FordonsTyp.Bat:
                        garage[f.ParkeringsIndex] = 1d;
                        garage[f.ParkeringsIndex + 1] = 1d;
                        garage[f.ParkeringsIndex + 2] = 1d;
                        antal += 3;
                        break;
                    case FordonsTyp.Bil:
                        garage[f.ParkeringsIndex] = 1d;
                        antal++;
                        break;
                    case FordonsTyp.Motorcykel:
                        garage[f.ParkeringsIndex] += 1 / 3d;
                        antal += 1 / 3d;
                        break;
                    case FordonsTyp.Buss:
                        garage[f.ParkeringsIndex] = 1d;
                        garage[f.ParkeringsIndex + 1] = 1d;
                        antal += 2;
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
                //LedigaPlatserFinns(fordonViewModel.FordonsTyp);

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
                    if(parkeratFordon != null)
                    {
                        parkeratFordon.FordonsTyp = parkeratFordonViewModel.FordonsTyp;
                        parkeratFordon.RegNr = parkeratFordonViewModel.RegNr;
                        parkeratFordon.Farg = parkeratFordonViewModel.Farg;
                        parkeratFordon.Marke = parkeratFordonViewModel.Marke;
                        parkeratFordon.Modell = parkeratFordonViewModel.Modell;
                        parkeratFordon.AntalHjul = parkeratFordonViewModel.AntalHjul;
                        _context.Update(parkeratFordon);

                        
                        TempData["OkFeedbackMsg"] = $"Uppdaterat fordon med reg nr {parkeratFordonViewModel.RegNr}";
                    }
                    await _context.SaveChangesAsync();
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
            int totalPris = RaknaUtPris(minutPris, tid);

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
            
        private TimeSpan RaknaUtTid (DateTime ankomst, DateTime utckeck)
        {
            return utckeck.Subtract(ankomst);
            
        }

        private int RaknaUtPris(int pris, TimeSpan parkeringstid)
        {
            return (int)parkeringstid.TotalMinutes * pris ;
        }

        private bool ParkeratFordonExists(int id)
        {
          return (_context.ParkeratFordon?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Filter(FordonOversiktViewModel fordonViewModel)
        {
            var query = string.IsNullOrWhiteSpace(fordonViewModel.RegNr) ?
                                               _context.ParkeratFordon :
                                               _context.ParkeratFordon.Where(p => p.RegNr.StartsWith(fordonViewModel.RegNr));


            var valdaFordon = await query.Select(v => new FordonOversiktViewModel
            {
                Id = v.Id,
                FordonsTyp = v.FordonsTyp,
                RegNr = v.RegNr,
                AnkomstTid = v.AnkomstTid
            }).ToListAsync();

            var fordon = new StartsidaViewModel            
            {
                ParkeradeFordon = valdaFordon,
                AntalLedigaPlatser = capacity - RaknaLedigaPlatser()

            };

            return View(nameof(Index), fordon);
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
            var parkeradeFordon = await _context.ParkeratFordon.ToListAsync();
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
            result.Intäkter = timeCalculator * minutPris;
            result.GenomsnittligParkeradTid = timeCalculator / divider;
            result.AntalBatar = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Bat)).Count();
            result.AntalBilar = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Bil)).Count();
            result.AntalBussar = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Buss)).Count();
            result.AntalFlygplan = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Flygplan)).Count();
            result.AntalMotorcyklar = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Motorcykel)).Count();
            return View(result);
        }

        private double RaknaLedigaPlatser()
        {
            var parkeradeFordon = _context.ParkeratFordon;
            double upptagnaPlatser = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Bat)).Count() * 3
                + parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Flygplan)).Count() * 3
                + parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Buss)).Count() * 2
                + parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Bil)).Count();
            double mcPlatser = parkeradeFordon.Where(p => p.FordonsTyp.Equals(FordonsTyp.Motorcykel)).Count();
            upptagnaPlatser += mcPlatser * .33;
            return upptagnaPlatser;

        }
    }
}