using NpvSpaApplication.Models;

namespace NpvSpaApplication.Service
{
    public interface IComputeService
    {
        NpvResultModel Npv(NpvDataModel model);
    }
}
