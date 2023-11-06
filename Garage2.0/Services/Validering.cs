using Garage2._0.Data;

namespace Garage2._0.Services
{
    public class Validering : IValidering
    {
        public bool RegNrExisterar(Garage2_0Context context, string regNr)
        {
            var fordonReg = context.ParkeratFordon.FirstOrDefault(v => v.RegNr == regNr);
            if (fordonReg == null)
            {
                return false;
            }
            return true;
        }
    }
}
