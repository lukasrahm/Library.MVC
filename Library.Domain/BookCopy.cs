using System.Collections.Generic;

namespace Library.Domain
{
    public class BookCopy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public bool OnLoan { get; set; }
    }
}