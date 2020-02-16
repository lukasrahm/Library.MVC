using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class AuthorsBooksVm
    {
        public ICollection<BookDetails> Books { get; set; } = new List<BookDetails>();
        public Author Author { get; set; }
    }
}
