using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookOrganizer.Models;
using BookOrganizer.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookOrganizer.Data
{
    public class BookOrganizerContext: IdentityDbContext<ApplicationUser>
    {
        public BookOrganizerContext(DbContextOptions<BookOrganizerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.EnableNotifications).HasDefaultValue(true);
            });

            modelBuilder.HasDefaultSchema("BookOrganizer");

        }
        public DbSet<BookOrganizer.Models.Book> Book { get; set; } = default!;
      


    }
}
