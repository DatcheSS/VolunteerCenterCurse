using Microsoft.EntityFrameworkCore;
using VolunteerCenterCurse.Components;
using VolunteerCenterCurse.Data;
using VolunteerCenterCurse.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=VolunteerCenter.db";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<ExpeditionService>();
builder.Services.AddScoped<NewsService>();
builder.Services.AddScoped<FormService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new() { Title = "Волонтёрский центр РТУ МИРЭА API", Version = "v1" }));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    await DataSeeder.SeedAsync(db);
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ВЦ МИРЭА API v1"));
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapGet("/api/volunteers", async (AppDbContext db) =>
{
    var vols = await db.Volunteers.Include(v => v.Curator).OrderBy(v => v.FullName).ToListAsync();
    return Results.Ok(new { count = vols.Count, data = vols });
})
.WithName("GetVolunteers")
.WithSummary("Список волонтёров");

app.MapGet("/api/events", async (AppDbContext db) =>
{
    var events = await db.Events.Include(e => e.Curator).OrderBy(e => e.Date).ToListAsync();
    return Results.Ok(new { count = events.Count, data = events });
})
.WithName("GetEvents")
.WithSummary("Список мероприятий");

app.MapGet("/api/expeditions", async (AppDbContext db) =>
{
    var exps = await db.Expeditions.Include(e => e.Leader).OrderBy(e => e.StartDate).ToListAsync();
    return Results.Ok(new { count = exps.Count, data = exps });
})
.WithName("GetExpeditions")
.WithSummary("Список экспедиций");

app.MapGet("/api/stats", async (AppDbContext db) =>
{
    return Results.Ok(new
    {
        volunteers = await db.Volunteers.CountAsync(),
        events = await db.Events.CountAsync(),
        expeditions = await db.Expeditions.CountAsync(),
        specialists = await db.Specialists.CountAsync()
    });
})
.WithName("GetStats")
.WithSummary("Статистика центра");

app.Run();
