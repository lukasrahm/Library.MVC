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

        public void AddNewBook(Book book)
        {
            context.Add(book);
            context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            context.Update(book);
            context.SaveChanges();
        }


        public IList<Book> GetAllBooks()
        {
            // Here we are NOT using .Include() so the authors books will NOT be loaded, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.Books.OrderBy(x => x.Id).Include(x => x.Author).Include(z => z.Copies).ToList();
        }

        public Book GetBook(int? id)
        {
            Book book = new Book();
            book = context.Books.Find(id);
            book.Copies = GetCopiesById(id);
            return book;
        }

        public void DeleteBook(Book book)
        {
            context.Remove(book);
            context.SaveChanges();

        }

        public IList<BookCopy> GetCopiesById(int? bookId)
        {
            return context.Copies.Where(copy => copy.BookId == bookId).ToList();
        }
        public IList<Book> GetBooksByAuthorId(int? authorId)
        {
            return context.Books.Where(book => book.AuthorId == authorId).ToList();
        }



        public void AddBookCopy(BookCopy copy)
        {
            context.Add(copy);
            context.SaveChanges();
        }

        public IList<BookCopy> GetAllBookCopies()
        {
            // Here we are using .Include() to eager load the author, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.Copies.Include(x => x.Book).ThenInclude(z => z.Author).OrderBy(x => x.Id).ToList();
        }

        public void LoanBookCopy(BookCopy copy)
        {
            context.Update(copy);
            context.SaveChanges();
        }
    }
}
