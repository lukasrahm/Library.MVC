using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IBookDetailsService
    {
        /// <summary>
        /// Adds the book to DB
        /// </summary>
        /// <param name="book"></param>
        void AddNewBook(BookDetails bookdetails);

        IList<BookDetails> GetAllBookDetails();
        BookDetails GetBookDetails(int? id);

        /// <summary>
        /// Updates a book.
        /// </summary>
        /// <param name="id">Id of book to update</param>
        /// <param name="book">New values of book (Id is ignored)</param>
        void UpdateBookDetails(BookDetails bookDetails);

        
        void DeleteBook(BookDetails bookDetails);
    }
}
