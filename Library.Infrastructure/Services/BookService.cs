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
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext context;

        public BookService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddBook(Book book)
        {
            context.Add(book);
            context.SaveChanges();
        }

        public ICollection<Book> GetAllBooks()
        {
            // Here we are using .Include() to eager load the author, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.Books.Include(x => x.Details).OrderBy(x => x.Details.Title).ToList();
        }

        public void UpdateBooks(Book book)
        {
            throw new NotImplementedException();
        }

        public void UpdateBooks(int id, Book book)
        {
            throw new NotImplementedException();
        }

        ICollection<Book> IBookService.GetAllBooks()
        {
            throw new NotImplementedException();
        }
    }
}
