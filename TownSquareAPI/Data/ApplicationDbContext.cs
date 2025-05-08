using Microsoft.EntityFrameworkCore;
using TownSquareAPI.Models;

namespace TownSquareAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<Post> Post { get; set; }
    public DbSet<Community> Community { get; set; }
    public DbSet<HelpPost> HelpPost { get; set; }
    public DbSet<Pin> Pin { get; set; }
    public DbSet<UserCommunity> UserCommunity { get; set; }
    public DbSet<CommunityPost> CommunityPost { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<HelpPost>()
            .Property(h => h.PostedAt)
            .HasColumnType("timestamp")
            .HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

        modelBuilder.Entity<HelpPost>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // user table
        //modelBuilder.Entity<User>().ToTable("user");

        // post table
        modelBuilder.Entity<Post>()
            .Property(p => p.PostedAt)
            .HasColumnType("timestamp")
            .HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

        modelBuilder.Entity<Post>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Post>()
            .HasOne<Community>()
            .WithMany()
            .HasForeignKey(p => p.CommunityId)
            .OnDelete(DeleteBehavior.Cascade);

        // community table
        //modelBuilder.Entity<Community>().ToTable("community");

        // pin table
        modelBuilder.Entity<Pin>()
            .Property(p => p.PostedAt)
            .HasColumnType("timestamp")
            .HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

        modelBuilder.Entity<Pin>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pin>()
            .HasOne<Community>()
            .WithMany()
            .HasForeignKey(p => p.CommunityId)
            .OnDelete(DeleteBehavior.Cascade);

        // user_community_membership table
        modelBuilder.Entity<UserCommunity>()
            .HasKey(ucr => new { ucr.UserId, ucr.CommunityId });

        modelBuilder.Entity<UserCommunity>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(ucr => ucr.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserCommunity>()
            .HasOne<Community>()
            .WithMany()
            .HasForeignKey(ucr => ucr.CommunityId)
            .OnDelete(DeleteBehavior.Cascade);

        // community_post table
        modelBuilder.Entity<CommunityPost>()
            .HasKey(cp => new { cp.CommunityId, cp.PostId });

        modelBuilder.Entity<CommunityPost>()
            .HasOne<Community>()
            .WithMany()
            .HasForeignKey(cp => cp.CommunityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommunityPost>()
            .HasOne<Post>()
            .WithMany()
            .HasForeignKey(cp => cp.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}