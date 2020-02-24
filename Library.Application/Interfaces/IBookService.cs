using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IBookService
    {

        #region Book
        /// <summary>
        /// Adds the book to DB
        /// </summary>
        /// <param name="book"></param>
        void AddNewBook(Book book);

        /// <summary>
        /// Returns all books in a list from db
        /// </summary>
        /// <returns>Books</returns>
        IList<Book> GetAllBooks();

        /// <summary>
        /// Get specific books from db
        /// </summary>
        /// <param name="id">Id of the book to return</param>
        /// <returns></returns>
        Book GetBook(int? id);

        /// <summary>
        /// Updates a book.
        /// </summary>
        /// <param name="book">New values of book</param>
        void UpdateBook(Book book);

        /// <summary>
        /// Delete a book from db
        /// </summary>
        /// <param name="book">Book to delete</param>
        void DeleteBook(Book book);

        /// <summary>
        /// Searches books in db by Title or ISBN
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Books the user searched for</returns>
        IList<Book> SearchBooks(string search);

        #endregion

        #region BookCopy

        /// <summary>
        /// Adds the bookcopy to DB
        /// </summary>
        /// <param name="book"></param>
        void AddBookCopy(BookCopy copy);

        /// <summary>
        /// Loan book
        /// </summary>
        /// <param name="copy">Which copy of a book to loan</param>
        void LoanBookCopy(BookCopy copy);

        /// <summary>
        /// Updates book copy, mainly used for the OnLoan variable
        /// </summary>
        /// <param name="copy">Which copy to update</param>
        void UpdateBookCopy(BookCopy copy);

        /// <summary>
        /// Returns specific bookcopy
        /// </summary>
        /// <param name="id">Which book to get from db</param>
        /// <returns>Specified book</returns>
        BookCopy GetBookCopy(int? id);

        /// <summary>
        /// Gets all books from the database
        /// </summary>
        /// <returns>List of books</returns>
        IList<BookCopy> GetAllBookCopies();

        /// <summary>
        /// Deletes specified book copy
        /// </summary>
        /// <param name="bookCopy">BookCopy to delete</param>
        void DeleteBookCopy(BookCopy bookCopy);

        /// <summary>
        /// Get all books from the database
        /// </summary>
        /// <param name="bookId">Copies of this book</param>
        /// <returns>Copies of specified book</returns>
        IList<BookCopy> GetAllBookCopies(int bookId);

        #endregion
    }
}
