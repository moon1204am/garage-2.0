﻿using Garage2._0.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models.ViewModels
{
    public class FordonViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Fordons typ")]
        public FordonsTyp FordonsTyp { get; set; }
        [Required]
        [Remote(action: "RegNrExisterar", controller: "ParkeratFordon", ErrorMessage = "Reg numret existerar redan, försök igen.")]
        [RegularExpression(@"[A-Za-z]{3}[0-9]{2}[A-Za-z0-9]{1}", ErrorMessage = "Ange ett giltigt reg nummer.")]
        [Display(Name = "Registreringsnummer")]
        public string RegNr { get; set; }
        [StringLength(20)]
        [Display(Name = "Färg")]
        public string Farg { get; set; }
        [StringLength(20)]
        [Display(Name = "Märke")]
        public string Marke { get; set; }
        [StringLength(20)]
        [Display(Name = "Modell")]
        public string Modell { get; set; }
        [Display(Name = "Antal hjul")]
        [Range(0, 12)]
        public int? AntalHjul { get; set; }
    }
}
