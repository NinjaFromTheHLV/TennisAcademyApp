using TennisAcademyApp.ViewModels.Reservation;

namespace TennisAcademyApp.Services.Core.Contracts
{
    public interface ISurfaceService
    {
        Task<IEnumerable<SurfaceDropDownModel>> GetSurfacesForDropDownAsync();
    }
}
