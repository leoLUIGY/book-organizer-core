using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookOrganizer.Models;

namespace BookOrganizer.Data
{
    public class BookOrganizerContext : DbContext
    {
        public BookOrganizerContext (DbContextOptions<BookOrganizerContext> options)
            : base(options)
        {
        }

        public DbSet<BookOrganizer.Models.Book> Book { get; set; } = default!;
    }
}
