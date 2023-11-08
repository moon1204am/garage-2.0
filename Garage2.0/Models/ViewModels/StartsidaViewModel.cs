using Garage2._0.Models.Entities;

namespace Garage2._0.Models.ViewModels
{
    public class StartsidaViewModel
    {
        public IEnumerable<FordonOversiktViewModel> ParkeradeFordon { get; set; } = new List<FordonOversiktViewModel>();
        public double AntalLedigaPlatser { get; set; }
    }
}
