using GTranslate.Translators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TennisAcademyApp.Data;
using TennisAcademyApp.Data.Models;
using TennisAcademyApp.Services.Core.Contracts;
using TennisAcademyApp.ViewModels.Coach;
using TennisAcademyApp.ViewModels.DropDown;
using TennisAcademyApp.ViewModels.Reservation;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Coach;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.User;
using static TennisAcademyApp.GCommon.Validations.ValidationConstants;
using static TennisAcademyApp.GCommon.Validations.ValidationConstants.Coach;

namespace TennisAcademyApp.Services.Core
{
    public class CoachService : ICoachService
    {
        private readonly TennisAcademyDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        public CoachService(TennisAcademyDbContext dbContext, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.configuration = configuration;
        }
        private string TransliterateToBg(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;

            var latinToCyrillic = new Dictionary<string, string>
    {
        {"Ch", "Ч"}, {"Ch'", "Ч"}, {"Sh", "Ш"}, {"Shch", "Щ"}, {"Zh", "Ж"}, {"Ya", "Я"}, {"Yu", "Ю"}, {"Yu'", "Ю"}, {"Ye", "Е"},
        {"ch", "ч"}, {"sh", "ш"}, {"shch", "щ"}, {"zh", "ж"}, {"ya", "я"}, {"yu", "ю"}, {"ye", "е"},
        {"A", "А"}, {"B", "Б"}, {"V", "В"}, {"G", "Г"}, {"D", "Д"}, {"E", "Е"}, {"Z", "З"}, {"I", "И"}, {"J", "Й"},
        {"K", "К"}, {"L", "Л"}, {"M", "М"}, {"N", "Н"}, {"O", "О"}, {"P", "П"}, {"R", "Р"}, {"S", "С"}, {"T", "Т"},
        {"U", "У"}, {"F", "Ф"}, {"H", "Х"}, {"C", "Ц"}, {"Y", "Й"}, {"X", "Х"}, {"W", "В"}, {"Q", "К"},
        {"a", "а"}, {"b", "б"}, {"v", "в"}, {"g", "г"}, {"d", "д"}, {"e", "е"}, {"z", "з"}, {"i", "и"}, {"j", "й"},
        {"k", "к"}, {"l", "л"}, {"m", "м"}, {"n", "н"}, {"o", "о"}, {"p", "п"}, {"r", "р"}, {"s", "с"}, {"t", "т"},
        {"u", "у"}, {"f", "ф"}, {"h", "х"}, {"c", "ц"}, {"y", "й"}, {"x", "х"}, {"w", "в"}, {"q", "к"}
    };

            foreach (var item in latinToCyrillic)
            {
                text = text.Replace(item.Key, item.Value);
            }

            return text;
        }
        public async Task<IEnumerable<CoachScheduleViewModel>> GetTrainerScheduleAsync(string userId)
        {
            return await dbContext.Reservations
                .Where(r => r.Coach.UserId == userId && !r.IsDeleted)
                .OrderBy(r => r.Date)
                .Select(r => new CoachScheduleViewModel
                {
                    ReservationId = r.Id,
                    Date = r.Date,
                    Duration = r.Duration,
                    Note = r.Note,
                    NoteBg = r.NoteBg,

                    SurfaceName = r.Surface.Name,
                    SurfaceNameBg = r.Surface.NameBg,

                    TrainingTypeName = r.TrainingType.Name,
                    TrainingTypeNameBg = r.TrainingType.NameBg,

                    PlayerName = r.Player.UserName ?? "",
                    PlayerEmail = r.Player.Email ?? ""
                })
                .ToListAsync();
        }
        public async Task<PaginatedCoachesViewModel> GetCoachesByPageAsync(string? searchQuery, int page, int pageSize)
        {
            var currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            bool isBg = currentCulture.Equals("bg", StringComparison.OrdinalIgnoreCase);

            var query = dbContext.Coaches.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(c => c.Name.Contains(searchQuery) || c.NameBg.Contains(searchQuery));
            }

            var totalCoaches = await query.CountAsync();

            var coaches = await query
                .OrderBy(c => c.CoachId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new AllCoachesViewModel
                {
                    CoachId = c.CoachId,
                    CoachName = isBg ? c.NameBg : c.Name,
                    CoachAge = c.Age,
                    ImageUrl = c.ImageUrl
                })
                .ToListAsync();

            foreach (var coach in coaches)
            {
                if (coach.ImageUrl.IsNullOrEmpty())
                {
                    coach.ImageUrl = NoImageUrl;
                }
            }

            var totalPages = (int)Math.Ceiling(totalCoaches / (double)pageSize);

            var model = new PaginatedCoachesViewModel
            {
                Coaches = coaches,
                PageNumber = page,
                TotalPages = totalPages,
                SearchQuery = searchQuery
            };

            return model;
        }

        public async Task<IEnumerable<CoachDropDownModel>> GetGoachesForDropDownAsync()
        {
            var coachesDropDown = await dbContext.Coaches
                .AsNoTracking()
                .Select(c => new CoachDropDownModel
                {
                    Id = c.CoachId,
                    Name = c.Name,
                    NameBg = c.NameBg,
                    ImageUrl = c.ImageUrl
                })
                .ToListAsync();

            return coachesDropDown;
        }

        public async Task<CoachDetailsViewModel> GetCoachDetailsAsync(string userId, int id)
        {
            CoachDetailsViewModel? coachDetails = null;

            var user = await userManager.FindByIdAsync(userId);
            var coach = await GetCoachByIdAsync(id);

            if (coach == null)
            {
                throw new ArgumentException(CoachNotFoundErrorMessage);
            }

            var currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            bool isBg = currentCulture.Equals("bg", StringComparison.OrdinalIgnoreCase);

            var coachReservations = await dbContext.Reservations
                .Where(r => r.CoachId == id && r.IsDeleted == false)
                .OrderBy(r => r.Date)
                .Select(r => new ReservationIndexViewModel
                {
                    ReservationId = r.Id,
                    Date = r.Date.ToString("dd.MM.yyyy HH:mm"),
                    TrainingTypeName = isBg ? r.TrainingType.NameBg : r.TrainingType.Name
                })
                .ToListAsync();

            coachDetails = new CoachDetailsViewModel
            {
                CoachId = coach.CoachId,
                CoachAge = coach.Age,
                ImageUrl = coach.ImageUrl,
                CoachReservations = coachReservations,

                CoachName = isBg ? coach.NameBg : coach.Name,
                Description = isBg ? coach.DescriptionBg : coach.Description,
                Nationality = isBg ? coach.NationalityBg : coach.Nationality,

                IsInUserFavorites = userId != null ?
                            await dbContext.UserFavourites.AnyAsync(uc => uc.UserId == userId
                            && uc.CoachId == coach.CoachId) : false
            };

            return coachDetails;
        }

        public async Task<bool> AddCoachAsync(string userId, AddCoachInputModel inputModel)
        {
            var adminUser = await userManager.FindByIdAsync(userId);
            bool isAdmin = await userManager.IsInRoleAsync(adminUser, "Admin");
            if (!isAdmin)
            {
                throw new ArgumentException("You must be an admin to add a coach.");
            }

            string coachEmail = $"{inputModel.Name.Replace(" ", ".").ToLower()}@tennis.com"
                .Replace("ö", "o").Replace("ä", "a").Replace("ü", "u");

            var existingUser = await userManager.FindByEmailAsync(coachEmail);
            if (existingUser != null)
            {
                throw new ArgumentException("A user with this coach email already exists.");
            }

            var coachUser = new ApplicationUser
            {
                Email = coachEmail,
                UserName = coachEmail,
                EmailConfirmed = true
            };
            string defaultPassword = configuration["CoachSettings:DefaultPassword"];
            var createResult = await userManager.CreateAsync(coachUser, defaultPassword);
            if (!createResult.Succeeded)
            {
                throw new Exception("Failed to create Identity User for the coach.");
            }

            await userManager.AddToRoleAsync(coachUser, Trainer);

            string nameBgResult = TransliterateToBg(inputModel.Name);

            var translator = new GoogleTranslator();
            var translatedDescription = await translator.TranslateAsync(inputModel.Description, "bg", "en");

            var translationContext = await translator.TranslateAsync($"He is {inputModel.Nationality.Trim()}", "bg", "en");
            string translatedPhrase = translationContext.Translation;
            string rawNationality = translatedPhrase.Split(' ').Last().Replace(".", "");
            string finalNationalityBg = char.ToUpper(rawNationality[0]) + rawNationality.Substring(1);

            var coach = new Data.Models.Coach
            {
                Name = inputModel.Name,
                NameBg = nameBgResult,
                ImageUrl = inputModel.ImageUrl ?? "~/pictures/DefaultUserImage.webp",
                Age = inputModel.Age,

                Nationality = inputModel.Nationality,
                NationalityBg = finalNationalityBg,

                Description = inputModel.Description,
                DescriptionBg = translatedDescription.Translation,
                IsDeleted = false,

                UserId = coachUser.Id
            };

            await dbContext.Coaches.AddAsync(coach);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<CoachEditInputModel> GetCoachForEdittingAsync(string userId, int id)
        {
            var user = await userManager.FindByIdAsync(userId);
            bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin || userId == null)
            {
                throw new ArgumentException("You must be an admin to edit a coach.");
            }

            if (user == null)
            {
                throw new ArgumentException(UserCannotBeNull);
            }
            var coach = await GetCoachByIdAsync(id);

            var model = new CoachEditInputModel
            {
                CoachId = coach.CoachId,
                Name = coach.Name,
                Age = coach.Age,
                Nationality = coach.Nationality,
                Description = coach.Description,
                ImageUrl = coach.ImageUrl,
            };
            return model;
        }

        public async Task<bool> EdittedCoachAsync(string userId, CoachEditInputModel model)
        {
            var user = await userManager.FindByIdAsync(userId);
            var coach = await dbContext.Coaches.FirstOrDefaultAsync(c => c.CoachId == model.CoachId);

            if (coach == null)
            {
                throw new ArgumentException(CoachNotFoundErrorMessage);
            }

            string nameBgResult = TransliterateToBg(model.Name);

            var translator = new GoogleTranslator();
            var translatedDescription = await translator.TranslateAsync(model.Description, "bg", "en");
            var translationContext = await translator.TranslateAsync($"He is {model.Nationality.Trim()}", "bg", "en");
            string translatedPhrase = translationContext.Translation;
            string rawNationality = translatedPhrase.Split(' ').Last().Replace(".", "");
            string finalNationalityBg = char.ToUpper(rawNationality[0]) + rawNationality.Substring(1);

            coach.Name = model.Name;
            coach.NameBg = nameBgResult;
            coach.Age = model.Age;
            coach.ImageUrl = model.ImageUrl;
            coach.Nationality = model.Nationality;
            coach.NationalityBg = finalNationalityBg;
            coach.Description = model.Description;
            coach.DescriptionBg = translatedDescription.Translation;

            dbContext.Entry(coach).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<DeleteCoachViewModel?> GetCoachForDeletingAsync(string userId, int id)
        {
            var user = await userManager.FindByIdAsync(userId);
            bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin || userId == null)
            {
                throw new ArgumentException("You must be an admin to delete a coach.");
            }

            var coach = await GetCoachByIdAsync(id);

            var model = new DeleteCoachViewModel
            {
                CoachId = coach.CoachId,
                Name = coach.Name,
                ImageUrl = coach.ImageUrl,
            };
            return model;
        }

        public async Task<bool> DeletedCoachAsync(string userId, DeleteCoachViewModel model)
        {
            var user = await userManager.FindByIdAsync(userId);
            var coach = await dbContext.Coaches.FindAsync(model.CoachId);

            if (coach == null)
            {
                throw new ArgumentException(CoachNotFoundErrorMessage);
            }

            dbContext.Coaches.Remove(coach);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<TennisAcademyApp.Data.Models.Coach?> GetCoachByIdAsync(int? id)
        {
            if (id.HasValue)
            {
                var coach = await dbContext.Coaches
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CoachId == id.Value);
                if (coach == null)
                {
                    throw new ArgumentException(CoachNotFoundErrorMessage);
                }
                return coach;
            }
            else
            {
                throw new ArgumentException(CoachCannotBeNullErrorMessage);
            }
        }
    }
}