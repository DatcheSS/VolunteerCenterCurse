using Microsoft.EntityFrameworkCore;
using VolunteerCenterCurse.Models;

namespace VolunteerCenterCurse.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Volunteer> Volunteers { get; set; } = null!;
    public DbSet<Specialist> Specialists { get; set; } = null!;
    public DbSet<Beneficiary> Beneficiaries { get; set; } = null!;
    public DbSet<VolunteerBeneficiary> VolunteerBeneficiaries { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<EventRegistration> EventRegistrations { get; set; } = null!;
    public DbSet<EventInvitation> EventInvitations { get; set; } = null!;
    public DbSet<Expedition> Expeditions { get; set; } = null!;
    public DbSet<News> News { get; set; } = null!;
    public DbSet<MediaOrder> MediaOrders { get; set; } = null!;
    public DbSet<VolunteerApplication> VolunteerApplications { get; set; } = null!;
    public DbSet<BeneficiaryRequest> BeneficiaryRequests { get; set; } = null!;
    public DbSet<CuratorNotification> CuratorNotifications { get; set; } = null!;
    public DbSet<ExpeditionParticipation> ExpeditionParticipations { get; set; } = null!;
    public DbSet<UserAchievement> UserAchievements { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Volunteer>()
            .HasOne(v => v.Curator)
            .WithMany(s => s.Volunteers)
            .HasForeignKey(v => v.CuratorId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Volunteer>()
            .HasIndex(v => v.Email)
            .IsUnique();

        modelBuilder.Entity<Specialist>()
            .HasIndex(s => s.Email)
            .IsUnique();

        modelBuilder.Entity<VolunteerBeneficiary>()
            .HasKey(vb => new { vb.VolunteerId, vb.BeneficiaryId });

        modelBuilder.Entity<VolunteerBeneficiary>()
            .HasOne(vb => vb.Volunteer)
            .WithMany(v => v.VolunteerBeneficiaries)
            .HasForeignKey(vb => vb.VolunteerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VolunteerBeneficiary>()
            .HasOne(vb => vb.Beneficiary)
            .WithMany(b => b.VolunteerBeneficiaries)
            .HasForeignKey(vb => vb.BeneficiaryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventRegistration>()
            .HasKey(er => new { er.VolunteerId, er.EventId });

        modelBuilder.Entity<EventRegistration>()
            .HasOne(er => er.Volunteer)
            .WithMany(v => v.EventRegistrations)
            .HasForeignKey(er => er.VolunteerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventRegistration>()
            .HasOne(er => er.Event)
            .WithMany(e => e.EventRegistrations)
            .HasForeignKey(er => er.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventInvitation>()
            .HasOne(ei => ei.Volunteer)
            .WithMany(v => v.EventInvitations)
            .HasForeignKey(ei => ei.VolunteerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventInvitation>()
            .HasOne(ei => ei.Event)
            .WithMany(e => e.EventInvitations)
            .HasForeignKey(ei => ei.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventInvitation>()
            .HasOne(ei => ei.Curator)
            .WithMany()
            .HasForeignKey(ei => ei.CuratorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Event>()
            .HasOne(e => e.Curator)
            .WithMany(s => s.Events)
            .HasForeignKey(e => e.CuratorId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Expedition>()
            .HasOne(exp => exp.Leader)
            .WithMany(s => s.Expeditions)
            .HasForeignKey(exp => exp.LeaderId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<CuratorNotification>()
            .HasOne(n => n.Curator)
            .WithMany()
            .HasForeignKey(n => n.CuratorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ExpeditionParticipation>()
            .HasIndex(ep => new { ep.VolunteerId, ep.ExpeditionId })
            .IsUnique();

        modelBuilder.Entity<ExpeditionParticipation>()
            .HasOne(ep => ep.Volunteer)
            .WithMany()
            .HasForeignKey(ep => ep.VolunteerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ExpeditionParticipation>()
            .HasOne(ep => ep.Expedition)
            .WithMany()
            .HasForeignKey(ep => ep.ExpeditionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
