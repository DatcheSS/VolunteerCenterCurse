using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VolunteerCenterCurse.Models;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    [Required]
    public string Location { get; set; } = null!;

    public int? CuratorId { get; set; }

    [JsonIgnore]
    public Specialist? Curator { get; set; }

    public string? ImageUrl { get; set; }

    [JsonIgnore]
    public bool IsPast => Date < DateTime.Now;

    [JsonIgnore]
    public ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

    [JsonIgnore]
    public ICollection<EventInvitation> EventInvitations { get; set; } = new List<EventInvitation>();
}
