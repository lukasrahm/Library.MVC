using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// Adds the book to DB
        /// </summary>
        /// <param name="book"></param>
        void AddBook(Book book);

        /// <summary>
        /// Updates a book
        /// </summary>
        /// <param name="book"></param>
        void UpdateBooks(Book book);

        /// <summary>
        /// Updates a book.
        /// </summary>
        /// <param name="id">Id of book to update</param>
        /// <param name="book">New values of book (Id is ignored)</param>
        void UpdateBooks(int id, Book book);
        /// <summary>
        /// Gets all books from the database
        /// </summary>
        /// <returns>list of books</returns>
        ICollection<Book> GetAllBooks();
    }
}
