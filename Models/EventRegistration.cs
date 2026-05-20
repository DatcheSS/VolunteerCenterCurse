using System.Text.Json.Serialization;

namespace VolunteerCenterCurse.Models;

public class EventRegistration
{
    public int VolunteerId { get; set; }
    public int EventId { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public Volunteer? Volunteer { get; set; }

    [JsonIgnore]
    public Event? Event { get; set; }
}
