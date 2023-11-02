using Garage2._0.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models.ViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Fordons typ")]
        public FordonsTyp FordonsTyp { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z]{3}[0-9]{2}[A-Za-z0-9]{1}")]
        [Display(Name = "Registreringsnummer")]
        public string RegNr { get; set; }
       

        public DateTime AnkomstTid { get; set; }
    }
}
