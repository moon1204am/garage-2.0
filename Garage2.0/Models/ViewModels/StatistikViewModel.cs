using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models.ViewModels
{
    public class StatistikViewModel
    {
        [Display(Name = "Intäkter")]
        public double Intakter {  get; set; }
        [Display(Name ="Parkerat tid i snitt")]
        public int GenomsnittligParkeradTid { get; set; }
        [Display(Name = "Antal hjul i garaget")]
        public int? AntalHjulIGaraget { get; set; }
        [Display(Name = "Antal flygplan")]
        public int AntalFlygplan {  get; set; }
        [Display(Name = "Antal motorcyklar")]
        public int AntalMotorcyklar {  get; set; }
        [Display(Name = "Antal bussar")]
        public int AntalBussar {  get; set; }
        [Display(Name = "Antal bilar")]
        public int AntalBilar {  get; set; }
        [Display(Name = "Antal båtar")]
        public int AntalBatar {  get; set; }

     
    }
}
