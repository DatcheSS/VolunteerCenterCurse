using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VolunteerCenterCurse.Models;

public class ExpeditionParticipation
{
    [Key]
    public int Id { get; set; }

    public int VolunteerId { get; set; }
    public int ExpeditionId { get; set; }
    public bool HasParticipated { get; set; }
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public Volunteer? Volunteer { get; set; }

    [JsonIgnore]
    public Expedition? Expedition { get; set; }
}
