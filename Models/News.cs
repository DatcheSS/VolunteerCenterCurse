using System.ComponentModel.DataAnnotations;

namespace VolunteerCenterCurse.Models;

public class News
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;
}
