using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VolunteerCenterCurse.Models;

public class Beneficiary
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string FullName { get; set; } = null!;

    [Required]
    public string Address { get; set; } = null!;

    [Phone]
    public string? Phone { get; set; }

    public string? Description { get; set; }

    [JsonIgnore]
    public ICollection<VolunteerBeneficiary> VolunteerBeneficiaries { get; set; } = new List<VolunteerBeneficiary>();
}
