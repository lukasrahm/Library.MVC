using Library.Application.Interfaces;
using Library.Domain;
using Library.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext context;

        public AuthorService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public void AddAuthor(Author author)
        {
            context.Add(author);
            context.SaveChanges();
        }

        public IList<Author> GetAllAuthors()
        {
            // Here we are NOT using .Include() so the authors books will NOT be loaded, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.Authors.OrderBy(x => x.Name).ToList();
        }

        public Author GetAuthorById(int? authorId)
        {
            Author author = context.Authors.Find(authorId);
            author.Books = context.Books.Where(x => x.AuthorId == authorId).ToList();
            return author;
        }

        public void UpdateAuthor(Author author)
        {
            context.Update(author);
            context.SaveChanges();
        }

        public IList<Author> SearchAuthors(string seaching)
        {
            return context.Authors.Where(x => x.Name.Contains(seaching)).ToList();
        }
    }
}
