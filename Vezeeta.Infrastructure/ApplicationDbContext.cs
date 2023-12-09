using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Vezeeta.Domain.Entities;

namespace Vezeeta.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole<int>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            builder.Entity<Doctor>().HasIndex(d => d.UserId).IsUnique();
            builder.Entity<DiscountCode>().HasIndex(e=>e.Code).IsUnique();
            builder.Entity<DiscountCode>()
                .HasMany(e => e.Users)
                .WithMany(e => e.DiscountCodes)
                .UsingEntity<DiscountCodeUser>();
            builder.Entity<DiscountCode>()
                .HasMany(e => e.Users)
                .WithMany(e => e.DiscountCodes)
                .UsingEntity<DiscountCodeUser>(
                    l => l.HasOne<User>(e => e.User).WithMany(e => e.DiscountCodeUsers),
                    r => r.HasOne<DiscountCode>(e => e.DiscountCode).WithMany(e => e.DiscountCodeUsers));
            builder.Entity<Reservation>()
                .HasOne(e => e.DiscountCodeUser)
                    .WithOne(e => e.Reservation)
                .HasForeignKey<DiscountCodeUser>(e => e.ReservationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<AppointmentsDay> AppointmentsDays { get; set; }
        public DbSet<AppointmentsHour> AppointmentsHours { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<DiscountCodeUser> DiscountCodesUsers { get; set;}

    }

}
