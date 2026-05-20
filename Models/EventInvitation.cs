using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VolunteerCenterCurse.Models;

public class EventInvitation
{
    [Key]
    public int Id { get; set; }

    public int VolunteerId { get; set; }
    public int EventId { get; set; }
    public int CuratorId { get; set; }

    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public bool IsAccepted { get; set; }

    [JsonIgnore]
    public Volunteer? Volunteer { get; set; }

    [JsonIgnore]
    public Event? Event { get; set; }

    [JsonIgnore]
    public Specialist? Curator { get; set; }
}
