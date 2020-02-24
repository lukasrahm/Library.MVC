using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class BookIndexVm
    {
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();

    }
}
