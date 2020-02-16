using System.Collections.Generic;

namespace Library.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public int DetailsId { get; set; }
        public BookDetails Details { get; set; }

        public bool OnLoan { get; set; }
    }
}