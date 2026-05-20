using System.ComponentModel.DataAnnotations;

namespace VolunteerCenterCurse.Models;

public class UserAchievement
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    public string UserRole { get; set; } = null!;
    public string AchievementKey { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
}
