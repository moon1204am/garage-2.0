using Garage2._0.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models.ViewModels
{
    public class StartsidaViewModel
    {
        public IEnumerable<FordonOversiktViewModel> ParkeradeFordon { get; set; } = new List<FordonOversiktViewModel>();
        [Display(Name = "Antal lediga platser")]
        public double AntalLedigaPlatser { get; set; }
    }
}
