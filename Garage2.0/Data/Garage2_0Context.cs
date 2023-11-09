using Microsoft.EntityFrameworkCore;
using Garage2._0.Models.Entities;

namespace Garage2._0.Data
{
    public class Garage2_0Context : DbContext
    {
        public Garage2_0Context (DbContextOptions<Garage2_0Context> options)
            : base(options)
        {
        }

        public DbSet<ParkeratFordon> ParkeratFordon => Set<ParkeratFordon>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParkeratFordon>().HasData(
                 new ParkeratFordon { Id = 1, FordonsTyp = FordonsTyp.Bil, RegNr = "ABC123", Farg = "Röd", Marke = "Toyota", Modell = "Prius", AntalHjul = 4, AnkomstTid = DateTime.Parse("2023-11-02T12:15") },
                 new ParkeratFordon { Id = 2, FordonsTyp = FordonsTyp.Bat, RegNr = "ABC124", Farg = "Vit", Marke = "Storebror", Modell = "Japp", AntalHjul = 0, AnkomstTid = DateTime.Parse("2023-11-02T12:15") },
                 new ParkeratFordon { Id = 3, FordonsTyp = FordonsTyp.Buss, RegNr = "ABC125", Farg = "Blå", Marke = "Volvo", Modell = "V70", AntalHjul = 6, AnkomstTid = DateTime.Parse("2023-11-02T12:45") },
                 new ParkeratFordon { Id = 4, FordonsTyp = FordonsTyp.Flygplan, RegNr = "ABC126", Farg = "Vit", Marke = "Airbus", Modell = "XX90", AntalHjul = 8, AnkomstTid = DateTime.Parse("2023-11-02T12:25") },
                 new ParkeratFordon { Id = 5, FordonsTyp = FordonsTyp.Motorcykel, RegNr = "ABC127", Farg = "Svart", Marke = "Mazda", Modell = "Vroom", AntalHjul = 2, AnkomstTid = DateTime.Parse("2023-11-02T12:55") }
                 );
        }
    }
}