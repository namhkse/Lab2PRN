﻿using Microsoft.EntityFrameworkCore;

namespace ODataBookStore.Models
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
            // Empty
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Press> Presses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().OwnsOne(c => c.Location);
        }
    }
}
