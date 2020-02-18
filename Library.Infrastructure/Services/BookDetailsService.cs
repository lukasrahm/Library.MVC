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
    public class BookDetailsService : IBookDetailsService
    {
        private readonly ApplicationDbContext context;

        public BookDetailsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddNewBook(BookDetails bookdetails)
        {
            context.Add(bookdetails);
            context.SaveChanges();
        }

        public void UpdateBookDetails(BookDetails book)
        {
            context.Update(book);
            context.SaveChanges();
        }


        public IList<BookDetails> GetAllBookDetails()
        {
            // Here we are NOT using .Include() so the authors books will NOT be loaded, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.BookDetails.OrderBy(x => x.Id).Include(x => x.Author).Include(z => z.Copies).ToList();
        }

        public BookDetails GetBookDetails(int? id)
        {
            BookDetails details = new BookDetails();
            details = context.BookDetails.Find(id);
            details.Copies = GetCopiesById(id);
            return details;
        }

        public void DeleteBook(BookDetails book)
        {
            context.Remove(book);
            context.SaveChanges();

        }

        public IList<Book> GetCopiesById(int? detailId)
        {
            return context.Books.Where(book => book.DetailsId == detailId).ToList();
        }
        public IList<BookDetails> GetBooksByAuthorId(int? authorId)
        {
            return context.BookDetails.Where(book => book.AuthorId == authorId).ToList();
        }
    }
}
