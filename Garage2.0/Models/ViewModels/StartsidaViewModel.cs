namespace Garage2._0.Models.ViewModels
{
    public class StartsidaViewModel
    {
        public IEnumerable<FordonOversiktViewModel> Fordon { get; set; } = new List<FordonOversiktViewModel>();
        public IEnumerable<decimal> ParkeringsOversikt { get; set; }
        public int AntalLedigaPlatser { get; set; }
    }
}
