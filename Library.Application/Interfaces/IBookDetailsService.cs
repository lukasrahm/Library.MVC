﻿using Library.Domain;
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
    }
}
