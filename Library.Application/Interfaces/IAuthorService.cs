using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IAuthorService
    {
        /// <summary>
        /// Gets all Authors
        /// </summary>
        /// <returns>Authors</returns>
        IList<Author> GetAllAuthors();

        /// <summary>
        /// Adds author to db
        /// </summary>
        /// <param name="author">Author to add</param>
        void AddAuthor(Author author);

        /// <summary>
        /// Get specific author
        /// </summary>
        /// <param name="authorId">Id of the author</param>
        /// <returns>Author</returns>
        Author GetAuthorById(int? authorId);

        /// <summary>
        /// Update author info
        /// </summary>
        /// <param name="author">Author info</param>
        void UpdateAuthor(Author author);

        /// <summary>
        /// Search authors in db
        /// </summary>
        /// <param name="search">user search</param>
        /// <returns>List with search result of authors</returns>
        IList<Author> SearchAuthors(string search);
    }
}
