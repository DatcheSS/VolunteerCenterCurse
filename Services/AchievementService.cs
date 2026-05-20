using Microsoft.EntityFrameworkCore;
using VolunteerCenterCurse.Data;
using VolunteerCenterCurse.Models;

namespace VolunteerCenterCurse.Services;

public class AchievementService
{
    private readonly AppDbContext _db;
    public AchievementService(AppDbContext db) => _db = db;

    private static readonly Dictionary<string, (string Title, string Description, string Icon)> Definitions = new()
    {
        ["first_event"]      = ("Юный волонтёр",   "Первое участие в мероприятии",        "bi-star-fill"),
        ["first_expedition"] = ("Юный экспедитор", "Первое участие в экспедиции",          "bi-compass"),
        ["accept_invite"]    = ("Да-да?",           "Принял(-а) приглашение от куратора",  "bi-hand-thumbs-up-fill"),
        ["send_invite"]      = ("Тук-тук!",         "Отправил(-а) приглашение волонтёру",  "bi-envelope-plus-fill"),
    };

    public async Task CheckAndAwardAsync(int userId, string role, string trigger)
    {
        if (!Definitions.ContainsKey(trigger)) return;

        var already = await _db.UserAchievements
            .AnyAsync(a => a.UserId == userId && a.UserRole == role && a.AchievementKey == trigger);
        if (already) return;

        var (title, desc, icon) = Definitions[trigger];
        _db.UserAchievements.Add(new UserAchievement
        {
            UserId = userId,
            UserRole = role,
            AchievementKey = trigger,
            Title = title,
            Description = desc,
            Icon = icon,
            EarnedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
    }

    public async Task<List<UserAchievement>> GetAchievementsAsync(int userId, string role)
    {
        return await _db.UserAchievements
            .Where(a => a.UserId == userId && a.UserRole == role)
            .OrderBy(a => a.EarnedAt)
            .ToListAsync();
    }

    public static IReadOnlyDictionary<string, (string Title, string Description, string Icon)> AllDefinitions => Definitions;
}
