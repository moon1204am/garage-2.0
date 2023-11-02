namespace Garage2._0.Models.Entities
{
    public class ParkeratFordon
    {
        public int Id { get; set; }
        public Enum FordonsTyp { get; set; }
        public string RegNr { get; set; }
        public string Farg { get; set; }
        public string Marke { get; set; }
        public string Modell { get; set; }
        public int AntalHjul { get; set; }
        public DateTime AnkomstTid { get; set; }



    }
}
