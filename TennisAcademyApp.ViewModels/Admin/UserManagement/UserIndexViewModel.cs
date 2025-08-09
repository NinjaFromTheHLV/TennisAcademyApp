namespace TennisAcademyApp.ViewModels.Admin.UserManagement
{
    public class UserIndexViewModel
    {
        public string Id { get; set; } = null!;
        public string? Email { get; set; }
        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
