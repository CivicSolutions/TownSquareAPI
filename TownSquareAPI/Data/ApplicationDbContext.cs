using TownSquareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TownSquareAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<HelpPost> HelpPosts { get; set; }
        public DbSet<PinData> Pins { get; set; }
        public DbSet<UserCommunityRequest> UserCommunityRequests { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HelpPost>().ToTable("help_posts");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Post>().ToTable("posts");
            modelBuilder.Entity<Community>().ToTable("communities");
            modelBuilder.Entity<PinData>().ToTable("pins");
            modelBuilder.Entity<UserCommunityRequest>().ToTable("user_community_requests");
            
        }
    }
}