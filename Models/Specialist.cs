using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VolunteerCenterCurse.Models;

public class Specialist
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "ФИО обязательно")]
    [StringLength(150)]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = "Некорректный номер телефона")]
    public string? Phone { get; set; }

    [Required]
    public string Position { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public string? PasswordHash { get; set; }

    [JsonIgnore]
    public ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();

    [JsonIgnore]
    public ICollection<Event> Events { get; set; } = new List<Event>();

    [JsonIgnore]
    public ICollection<Expedition> Expeditions { get; set; } = new List<Expedition>();
}
