using Microsoft.EntityFrameworkCore;
using VolunteerCenterCurse.Data;
using VolunteerCenterCurse.Models;

namespace VolunteerCenterCurse.Services;

public class ExpeditionService
{
    private readonly AppDbContext _db;
    public ExpeditionService(AppDbContext db) => _db = db;

    public async Task<List<Expedition>> GetAllAsync()
    {
        return await _db.Expeditions
            .Include(e => e.Leader)
            .OrderBy(e => e.StartDate)
            .ToListAsync();
    }

    public async Task RequestParticipationAsync(int volunteerId, int expeditionId)
    {
        var exists = await _db.ExpeditionParticipations
            .AnyAsync(ep => ep.VolunteerId == volunteerId && ep.ExpeditionId == expeditionId);
        if (!exists)
        {
            _db.ExpeditionParticipations.Add(new ExpeditionParticipation
            {
                VolunteerId = volunteerId,
                ExpeditionId = expeditionId,
                HasParticipated = false,
                RequestedAt = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
        }
    }

    public async Task<List<ExpeditionParticipation>> GetVolunteerParticipationsAsync(int volunteerId)
    {
        return await _db.ExpeditionParticipations
            .Include(ep => ep.Expedition)
            .Where(ep => ep.VolunteerId == volunteerId)
            .OrderByDescending(ep => ep.Expedition!.StartDate)
            .ToListAsync();
    }

    public async Task<HashSet<int>> GetRequestedExpeditionIdsAsync(int volunteerId)
    {
        var ids = await _db.ExpeditionParticipations
            .Where(ep => ep.VolunteerId == volunteerId)
            .Select(ep => ep.ExpeditionId)
            .ToListAsync();
        return ids.ToHashSet();
    }
}
