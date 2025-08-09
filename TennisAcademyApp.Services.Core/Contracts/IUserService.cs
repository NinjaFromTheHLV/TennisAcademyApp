using TennisAcademyApp.ViewModels.Admin.UserManagement;

namespace TennisAcademyApp.Services.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserIndexViewModel>> GetUserManagementDataAsync(string userId);
        //Task<IEnumerable<string>> GetManagerEmailsAsync();
        //Task<bool> AssignUserToRoleAsync(RoleSelectionInputModel inputModel);
    }
}
