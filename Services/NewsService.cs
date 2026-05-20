using Microsoft.EntityFrameworkCore;
using VolunteerCenterCurse.Data;
using VolunteerCenterCurse.Models;

namespace VolunteerCenterCurse.Services;

public class NewsService
{
    private readonly AppDbContext _db;
    public NewsService(AppDbContext db) => _db = db;

    public async Task<List<News>> GetActiveNewsAsync()
    {
        return await _db.News
            .Where(n => n.IsActive)
            .OrderByDescending(n => n.PublishedAt)
            .ToListAsync();
    }
}
