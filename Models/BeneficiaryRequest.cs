using System.ComponentModel.DataAnnotations;

namespace VolunteerCenterCurse.Models;

public class BeneficiaryRequest
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "ФИО обязательно")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Название организации обязательно")]
    public string Organization { get; set; } = null!;

    [Required(ErrorMessage = "Описание обязательно")]
    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
