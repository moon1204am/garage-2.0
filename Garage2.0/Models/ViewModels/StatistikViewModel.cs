using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models.ViewModels
{
    public class StatistikViewModel
    {
        public double Intäkter {  get; set; }
        [Display(Name ="Genomsnittlig parkerad tid")]
        public double GenomsnittligParkeradTid { get; set; }
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
