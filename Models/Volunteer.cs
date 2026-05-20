using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VolunteerCenterCurse.Models;

public class Volunteer
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "ФИО обязательно")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "ФИО должно быть от 3 до 150 символов")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = "Некорректный номер телефона")]
    public string? Phone { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Description { get; set; }

    public string? AvatarUrl { get; set; }

    public string? PasswordHash { get; set; }

    public int? CuratorId { get; set; }

    [JsonIgnore]
    public Specialist? Curator { get; set; }

    [JsonIgnore]
    public ICollection<VolunteerBeneficiary> VolunteerBeneficiaries { get; set; } = new List<VolunteerBeneficiary>();

    [JsonIgnore]
    public ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

    [JsonIgnore]
    public ICollection<EventInvitation> EventInvitations { get; set; } = new List<EventInvitation>();
}
