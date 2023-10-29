using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoDashboard
    {
        Task<ResponseObject> GetDashboard(FiltrosDTO filtros);
    }
}
