using VolunteerCenterCurse.Data;
using VolunteerCenterCurse.Models;

namespace VolunteerCenterCurse.Services;

public class FormService
{
    private readonly AppDbContext _db;
    public FormService(AppDbContext db) => _db = db;

    public async Task SubmitMediaOrderAsync(string fullName, string requirements)
    {
        _db.MediaOrders.Add(new MediaOrder { FullName = fullName, Requirements = requirements, CreatedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();
    }

    public async Task SubmitVolunteerApplicationAsync(string fullName, string phone, string? contactMethod)
    {
        _db.VolunteerApplications.Add(new VolunteerApplication
        {
            FullName = fullName,
            Phone = phone,
            ContactMethod = contactMethod,
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
    }

    public async Task SubmitBeneficiaryRequestAsync(string fullName, string organization, string description)
    {
        _db.BeneficiaryRequests.Add(new BeneficiaryRequest
        {
            FullName = fullName,
            Organization = organization,
            Description = description,
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
    }

    public async Task UpdateVolunteerProfileAsync(int volunteerId, string? description, string? avatarUrl, AppDbContext db)
    {
        var vol = await db.Volunteers.FindAsync(volunteerId);
        if (vol != null)
        {
            if (description != null) vol.Description = description;
            if (avatarUrl != null) vol.AvatarUrl = avatarUrl;
            await db.SaveChangesAsync();
        }
    }
}
