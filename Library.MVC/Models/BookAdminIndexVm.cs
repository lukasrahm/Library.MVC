using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class BookAdminIndexVm
    {
        public ICollection<BookDetails> BookDetails { get; set; } = new List<BookDetails>();
        public ICollection<Author> Authors { get; set; } = new List<Author>();

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
