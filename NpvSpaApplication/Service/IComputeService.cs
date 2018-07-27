using NpvSpaApplication.Models;

namespace NpvSpaApplication.Service
{
    public interface IComputeService
    {
        NpvResultModel NpvCollection(NpvObjectModel model);

        double Npv(NpvObjectModel model, double discountRate);
    }
}
