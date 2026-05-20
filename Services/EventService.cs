using Microsoft.EntityFrameworkCore;
using VolunteerCenterCurse.Data;
using VolunteerCenterCurse.Models;

namespace VolunteerCenterCurse.Services;

public class EventService
{
    private readonly AppDbContext _db;
    public EventService(AppDbContext db) => _db = db;

    public async Task<List<Event>> GetEventsThisMonthAsync()
    {
        var now = DateTime.Now;
        var start = new DateTime(now.Year, now.Month, 1);
        var end = start.AddMonths(1);
        return await _db.Events
            .Include(e => e.Curator)
            .Where(e => e.Date >= start && e.Date < end)
            .OrderBy(e => e.Date)
            .ToListAsync();
    }

    public async Task<List<Event>> GetUpcomingEventsAsync(int count = 3)
    {
        var now = DateTime.Now;
        return await _db.Events
            .Include(e => e.Curator)
            .Where(e => e.Date >= now)
            .OrderBy(e => e.Date)
            .Take(count)
            .ToListAsync();
    }

    public async Task<Event?> GetEventByIdAsync(int id)
    {
        return await _db.Events
            .Include(e => e.Curator)
            .Include(e => e.EventRegistrations)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> IsRegisteredAsync(int volunteerId, int eventId)
    {
        return await _db.EventRegistrations
            .AnyAsync(er => er.VolunteerId == volunteerId && er.EventId == eventId);
    }

    public async Task RegisterAsync(int volunteerId, int eventId)
    {
        if (!await IsRegisteredAsync(volunteerId, eventId))
        {
            _db.EventRegistrations.Add(new EventRegistration
            {
                VolunteerId = volunteerId,
                EventId = eventId,
                RegisteredAt = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
        }
    }

    public async Task UnregisterAsync(int volunteerId, int eventId)
    {
        var reg = await _db.EventRegistrations
            .FirstOrDefaultAsync(er => er.VolunteerId == volunteerId && er.EventId == eventId);
        if (reg == null) return;

        var ev = await _db.Events.FindAsync(eventId);
        var vol = await _db.Volunteers.FindAsync(volunteerId);
        _db.EventRegistrations.Remove(reg);

        if (ev?.CuratorId != null && vol != null)
        {
            _db.CuratorNotifications.Add(new CuratorNotification
            {
                CuratorId = ev.CuratorId.Value,
                Message = $"{vol.FullName} отказался(-ась) от участия в мероприятии «{ev.Title}»",
                CreatedAt = DateTime.UtcNow
            });
        }

        await _db.SaveChangesAsync();
    }

    public async Task<List<Event>> GetRegisteredEventsAsync(int volunteerId)
    {
        var now = DateTime.Now;
        return await _db.EventRegistrations
            .Include(er => er.Event).ThenInclude(e => e!.Curator)
            .Where(er => er.VolunteerId == volunteerId && er.Event!.Date >= now)
            .Select(er => er.Event!)
            .OrderBy(e => e.Date)
            .ToListAsync();
    }

    public async Task<List<Event>> GetAttendedEventsAsync(int volunteerId)
    {
        var now = DateTime.Now;
        return await _db.EventRegistrations
            .Include(er => er.Event)
            .Where(er => er.VolunteerId == volunteerId && er.Event!.Date < now)
            .Select(er => er.Event!)
            .OrderByDescending(e => e.Date)
            .ToListAsync();
    }

    public async Task<List<EventInvitation>> GetInvitationsAsync(int volunteerId)
    {
        return await _db.EventInvitations
            .Include(ei => ei.Event)
            .Include(ei => ei.Curator)
            .Where(ei => ei.VolunteerId == volunteerId && !ei.IsAccepted)
            .OrderByDescending(ei => ei.SentAt)
            .ToListAsync();
    }

    public async Task SendInvitationAsync(int volunteerId, int eventId, int curatorId)
    {
        var exists = await _db.EventInvitations
            .AnyAsync(ei => ei.VolunteerId == volunteerId && ei.EventId == eventId);
        if (!exists)
        {
            _db.EventInvitations.Add(new EventInvitation
            {
                VolunteerId = volunteerId,
                EventId = eventId,
                CuratorId = curatorId,
                SentAt = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
        }
    }

    public async Task AcceptInvitationAsync(int invitationId, int volunteerId)
    {
        var inv = await _db.EventInvitations.FindAsync(invitationId);
        if (inv != null && inv.VolunteerId == volunteerId)
        {
            inv.IsAccepted = true;
            await _db.SaveChangesAsync();
            await RegisterAsync(volunteerId, inv.EventId);
        }
    }

    public async Task<List<Volunteer>> GetAllVolunteersAsync()
    {
        return await _db.Volunteers.OrderBy(v => v.FullName).ToListAsync();
    }

    public async Task<List<Event>> GetAllUpcomingAsync()
    {
        var now = DateTime.Now;
        return await _db.Events
            .Where(e => e.Date >= now)
            .OrderBy(e => e.Date)
            .ToListAsync();
    }

    public async Task<List<CuratorNotification>> GetCuratorNotificationsAsync(int curatorId)
    {
        return await _db.CuratorNotifications
            .Where(n => n.CuratorId == curatorId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task MarkNotificationsReadAsync(int curatorId)
    {
        var list = await _db.CuratorNotifications
            .Where(n => n.CuratorId == curatorId && !n.IsRead)
            .ToListAsync();
        foreach (var n in list) n.IsRead = true;
        await _db.SaveChangesAsync();
    }
}
