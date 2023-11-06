using Garage2._0.Data;

namespace Garage2._0.Services
{
    public interface IValidering
    {
        bool RegNrExisterar(Garage2_0Context context, string regNr);
    }
}