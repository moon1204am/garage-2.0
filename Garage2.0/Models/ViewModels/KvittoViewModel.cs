using System.ComponentModel.DataAnnotations;


namespace Garage2._0.Models.ViewModels
{
    public class KvittoViewModel
    {
        public  int Id  { get; set; }

        [Display(Name = "Registreringsnummer")]
        public string RegNr { get; set; }

        [Display(Name = "Ankomst tid")]
        public DateTime AnkomstTid { get; set; }

        [Display(Name = "Utcheckning tid")]
        public DateTime UtchecksTid { get; set; }

        [Display(Name = "Total parkeringstid")]
        public string ParkeringsTid { get; set; }
        
        [Display(Name = "Pris/timme")]
        public int Pris { get; set; }

        [Display(Name = "Total pris")]
        public  int TotalPris { get; set; }


    }
}
