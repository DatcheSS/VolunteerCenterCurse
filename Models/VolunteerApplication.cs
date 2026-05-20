using System.ComponentModel.DataAnnotations;

namespace VolunteerCenterCurse.Models;

public class VolunteerApplication
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "ФИО обязательно")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Номер телефона обязателен")]
    [Phone(ErrorMessage = "Некорректный номер телефона")]
    public string Phone { get; set; } = null!;

    public string? ContactMethod { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
