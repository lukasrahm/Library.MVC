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
            try
            {
                Book book = new Book();
                book = context.Books.Find(id);
                if(book != null)
                {
                    book.Copies = context.Copies.Where(x => x.BookId == id).ToList();
                    book.Author = context.Authors.Find(book.AuthorId);
                }

                return book;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void DeleteBook(Book book)
        {
            context.Remove(book);
            context.SaveChanges();

        }


        public void AddBookCopy(BookCopy copy)
        {
            context.Add(copy);
            context.SaveChanges();
        }

        public void UpdateBookCopy(BookCopy copy)
        {
            context.Update(copy);
            context.SaveChanges();
        }

        public IList<BookCopy> GetAllBookCopies()
        {
            // Here we are using .Include() to eager load the author, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.Copies.Include(x => x.Book).ThenInclude(z => z.Author).OrderBy(x => x.Id).ToList();
        }

        public IList<BookCopy> GetAllBookCopies(int bookId)
        {
            return context.Copies.Where(x => x.BookId == bookId).OrderBy(x => x.Id).ToList();
        }

        public void LoanBookCopy(BookCopy copy)
        {
            context.Update(copy);
            context.SaveChanges();
        }

        public BookCopy GetBookCopy(int? id)
        {
            return context.Copies.Find(id);
        }

        public void DeleteBookCopy(BookCopy bookCopy)
        {
            context.Remove(bookCopy);
            context.SaveChanges();

        }

        public IList<Book> SearchBooks(string search)
        {
            if (search.All(c => char.IsDigit(c)))
            {
                if (search.Length == 10 || search.Length == 13)  //If search is in ISBN format
                {
                    //searching for book ISBN
                    return context.Books.Where(x => x.ISBN == search).ToList(); //Return list with ISBN search
                }
            }

            //searching for book title
            return context.Books.Where(x => x.Title.Contains(search)).ToList(); //Return list with title search
        }
    }
}
