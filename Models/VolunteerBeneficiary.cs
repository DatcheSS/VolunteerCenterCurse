using System.Text.Json.Serialization;

namespace VolunteerCenterCurse.Models;

public class VolunteerBeneficiary
{
    public int VolunteerId { get; set; }
    public int BeneficiaryId { get; set; }
    public DateTime AssignmentDate { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public Volunteer? Volunteer { get; set; }

    [JsonIgnore]
    public Beneficiary? Beneficiary { get; set; }
}
