﻿using Library.Application.Interfaces;
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
            return context.BookDetails.OrderBy(x => x.Id).ToList();
        }

        public BookDetails GetBookDetails(int? id)
        {
            return context.BookDetails.Find(id);
        }

        public void DeleteBook(BookDetails book)
        {
            context.Remove(book);
            context.SaveChanges();

        }
    }
}