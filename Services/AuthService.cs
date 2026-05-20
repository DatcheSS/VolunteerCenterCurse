using Microsoft.EntityFrameworkCore;
using VolunteerCenterCurse.Data;

namespace VolunteerCenterCurse.Services;

public class AuthService
{
    public event Action? OnStateChanged;

    public bool IsLoggedIn { get; private set; }
    public int UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string UserRole { get; private set; } = string.Empty;

    public bool IsVolunteer => UserRole == "volunteer";
    public bool IsCurator => UserRole == "curator";

    public void Initialize(int userId, string role, string name)
    {
        UserId = userId;
        UserRole = role;
        UserName = name;
        IsLoggedIn = true;
        OnStateChanged?.Invoke();
    }

    public void Logout()
    {
        IsLoggedIn = false;
        UserId = 0;
        UserName = string.Empty;
        UserRole = string.Empty;
        OnStateChanged?.Invoke();
    }

    public async Task<bool> LoginAsync(string email, string password, AppDbContext db)
    {
        var hash = HashPassword(password);

        var volunteer = await db.Volunteers
            .FirstOrDefaultAsync(v => v.Email == email && v.PasswordHash == hash);
        if (volunteer != null)
        {
            Initialize(volunteer.Id, "volunteer", volunteer.FullName);
            return true;
        }

        var specialist = await db.Specialists
            .FirstOrDefaultAsync(s => s.Email == email && s.PasswordHash == hash);
        if (specialist != null)
        {
            Initialize(specialist.Id, "curator", specialist.FullName);
            return true;
        }

        return false;
    }

    public static string HashPassword(string password)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        return Convert.ToHexString(sha.ComputeHash(bytes)).ToLowerInvariant();
    }
}
