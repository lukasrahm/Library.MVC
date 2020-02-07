using Library.Application.Interfaces;
using Library.Domain;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Services
{
    public class NewBookService : IBookDetailsService
    {
        private readonly ApplicationDbContext context;

        public NewBookService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddNewBook(BookDetails bookdetails)
        {
            context.Add(bookdetails);
            context.SaveChanges();
        }

        public ICollection<BookDetails> GetAllBooks()
        {
            // Here we are using .Include() to eager load the author, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.BookDetails.Include(x => x.Author).OrderBy(x => x.Title).ToList();
        }

        public void UpdateBookDetails(BookDetails book)
        {
            throw new NotImplementedException();
        }

        public void UpdateBookDetails(int id, BookDetails book)
        {
            throw new NotImplementedException();
        }

        ICollection<BookDetails> IBookDetailsService.GetAllBookDetails()
        {
            throw new NotImplementedException();
        }
    }
}
