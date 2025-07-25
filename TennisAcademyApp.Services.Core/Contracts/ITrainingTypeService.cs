using TennisAcademyApp.ViewModels.Reservation;
namespace TennisAcademyApp.Services.Core.Contracts
{
    public interface ITrainingTypeService
    {
        Task<IEnumerable<TrainingTypeDropDownModel>> GetAllTrainingTypesForDropDownAsync();
    }
}
