using System.ComponentModel.DataAnnotations;

namespace VolunteerCenterCurse.Models;

public class MediaOrder
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "ФИО обязательно")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Укажите требования")]
    public string Requirements { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
