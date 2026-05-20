using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VolunteerCenterCurse.Models;

public class Expedition
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [Required]
    public string Location { get; set; } = null!;

    public int? LeaderId { get; set; }

    [JsonIgnore]
    public Specialist? Leader { get; set; }

    public string? HousingConditions { get; set; }

    public int MaxParticipants { get; set; }

    public string? ImageUrl { get; set; }
}
