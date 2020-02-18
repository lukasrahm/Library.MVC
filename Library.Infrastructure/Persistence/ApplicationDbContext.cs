using Library.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext() : base()
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<BookDetails> BookDetails { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureMember(modelBuilder);
            ConfigureAuthor(modelBuilder);
            ConfigureBookDetails(modelBuilder);
            ConfigureBook(modelBuilder);
            ConfigureLoan(modelBuilder);


            SeedDatabase(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "William Shakespeare" },
                new Author { Id = 2, Name = "Villiam Skakspjut" },
                new Author { Id = 3, Name = "Robert C. Martin" }
                );
            modelBuilder.Entity<BookDetails>().HasData(
                new BookDetails { Id = 1, AuthorId = 1, Title = "Hamlet", ISBN = "1472518381", Description = "Arguably Shakespeare's greatest tragedy" },
                new BookDetails { Id = 2, AuthorId = 1, Title = "King Lear", ISBN = "9780141012292", Description = "King Lear is a tragedy written by William Shakespeare. It depicts the gradual descent into madness of the title character, after he disposes of his kingdom by giving bequests to two of his three daughters egged on by their continual flattery, bringing tragic consequences for all." },
                new BookDetails { Id = 3, AuthorId = 2, Title = "Othello", ISBN = "1853260185", Description = "An intense drama of love, deception, jealousy and destruction." }
                );
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, DetailsId = 1 },
                new Book { Id = 2, DetailsId = 1 },
                new Book { Id = 3, DetailsId = 1 },
                new Book { Id = 4, DetailsId = 2 },
                new Book { Id = 5, DetailsId = 3 }
                );
            modelBuilder.Entity<Member>().HasData(
                new Member { Id = 1, Name = "Lukas Rahm", SSN = "199801280919" }
                );
            modelBuilder.Entity<Loan>().HasData(
                new Loan { Id = 1, BookId = 1, MemberId = 1, DateOfLoan = DateTime.Today.ToLocalTime(), DateOfReturn = DateTime.Today.ToLocalTime().AddDays(14) }
                );
        }

        private static void ConfigureAuthor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().Property(x => x.Name).HasMaxLength(55);
        }


        private static void ConfigureBook(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(x => x.Id);
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Details)
                .WithMany(a => a.Copies)
                .HasForeignKey(b => b.DetailsId);
        }
        private static void ConfigureBookDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDetails>().HasKey(x => x.Id);
            modelBuilder.Entity<BookDetails>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
        }

        private static void ConfigureLoan(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>().HasKey(x => x.Id);
            modelBuilder.Entity<Loan>()
                .HasOne(b => b.Member)
                .WithMany(a => a.Loans)
                .HasForeignKey(b => b.MemberId);
        }

        private static void ConfigureMember(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().HasKey(x => x.Id);
        }


    }
}
