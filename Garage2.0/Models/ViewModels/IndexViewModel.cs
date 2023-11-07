using Garage2._0.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models.ViewModels
{
    public class IndexViewModel
    {

        public IEnumerable<IndexViewModel> ParkeradeFordon { get; set; } = new List<IndexViewModel>();
        public int AntalLedigaPlatser {  get; set; }
        public int Id { get; set; }
       
        public FordonsTyp FordonsTyp { get; set; }
        [Required]
        [Remote(action: "RegNrExisterar", controller: "ParkeratFordon")]
        [RegularExpression(@"[A-Za-z]{3}[0-9]{2}[A-Za-z0-9]{1}")]
        [Display(Name = "Registreringsnummer")]
        public string RegNr { get; set; }

        public DateTime AnkomstTid { get; set; }


    }
}
