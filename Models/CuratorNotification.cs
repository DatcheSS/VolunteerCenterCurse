using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VolunteerCenterCurse.Models;

public class CuratorNotification
{
    [Key]
    public int Id { get; set; }

    public int CuratorId { get; set; }

    [Required]
    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; }

    [JsonIgnore]
    public Specialist? Curator { get; set; }
}
