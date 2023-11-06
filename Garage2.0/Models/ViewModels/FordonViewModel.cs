using Garage2._0.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models.ViewModels
{
    public class FordonViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ange fordonstyp.")]
        [Display(Name = "Fordons typ")]
        public FordonsTyp FordonsTyp { get; set; }
        [Required(ErrorMessage = "Ange registreringnummer.")]
        [Remote(action: "RegNrExisterar", controller: "ParkeratFordon", ErrorMessage = "Registreringsnumret existerar redan, försök igen.")]
        [RegularExpression(@"[A-Za-z]{3}[0-9]{2}[A-Za-z0-9]{1}", ErrorMessage = "Ange ett giltigt registreringsnummer.")]
        [Display(Name = "Registreringsnummer")]
        public string RegNr { get; set; }
        [Required(ErrorMessage = "Ange färg.")]
        [StringLength(20)]
        [Display(Name = "Färg")]
        public string Farg { get; set; }
        [Required(ErrorMessage = "Ange märke.")]
        [StringLength(20)]
        [Display(Name = "Märke")]
        public string Marke { get; set; }
        [Required(ErrorMessage = "Ange modell.")]
        [StringLength(20)]
        [Display(Name = "Modell")]
        public string Modell { get; set; }
        [Required(ErrorMessage = "Ange antal hjul.")]
        [Display(Name = "Antal hjul")]
        [Range(0, 12)]
        public int? AntalHjul { get; set; }
    }
}