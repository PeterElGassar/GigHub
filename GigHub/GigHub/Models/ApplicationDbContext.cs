using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<User_Notification> User_Notifications { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Turn Off Cascade Delete
            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Gig)
                .WithMany(g => g.Attendances)
                .WillCascadeOnDelete(false);

            //Turn Off Cascade Delete Following table
            modelBuilder.Entity<Following>()
                .HasRequired(f => f.Followee)
                .WithMany(u => u.Followees)
                .WillCascadeOnDelete(false);


            //Turn Off Cascade Delete Following table
            modelBuilder.Entity<Following>()
                .HasRequired(f => f.Follower)
                .WithMany(u => u.Followers)
                .WillCascadeOnDelete(false);


            //Turn Off Cascade Delete User_Notification table
            modelBuilder.Entity<User_Notification>()
                .HasRequired(un => un.User)
                .WithMany(u=> u.UserNotifications)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}