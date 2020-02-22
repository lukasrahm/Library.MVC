﻿using Library.Domain;
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
        /// <returns>List of authors</returns>
        IList<Author> GetAllAuthors();

        /// <summary>
        /// Adds the author to DB
        /// </summary>
        /// <param name="author"></param>
        void AddAuthor(Author author);
        Author GetAuthorById(int? authorId);

        void UpdateAuthor(Author author);

        IList<Author> SearchAuthors(string searching);
    }
}
