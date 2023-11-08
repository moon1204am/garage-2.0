using Garage2._0.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models.ViewModels
{
    public class FordonOversiktViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Fordonstyp")]
        public FordonsTyp FordonsTyp { get; set; }
        [Display(Name = "Registreringsnummer")]
        public string RegNr { get; set; } = string.Empty;
        [Display(Name = "Ankomsttid")]
        public DateTime AnkomstTid { get; set; }
    }
}