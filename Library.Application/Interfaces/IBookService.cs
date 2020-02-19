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
        void AddNewBook(Book book);

        IList<Book> GetAllBooks();
        Book GetBook(int? id);

        /// <summary>
        /// Updates a book.
        /// </summary>
        /// <param name="id">Id of book to update</param>
        /// <param name="book">New values of book (Id is ignored)</param>
        void UpdateBook(Book book);


        void DeleteBook(Book book);

        IList<BookCopy> GetCopiesById(int? bookId);

        IList<Book> GetBooksByAuthorId(int? id);




        /// <summary>
        /// Adds the book to DB
        /// </summary>
        /// <param name="book"></param>
        void AddBookCopy(BookCopy copy);

        void LoanBookCopy(BookCopy copy);

        void UpdateBookCopy(BookCopy copy);

        /// <summary>
        /// Gets all books from the database
        /// </summary>
        /// <returns>list of books</returns>
        IList<BookCopy> GetAllBookCopies();
    }
}
