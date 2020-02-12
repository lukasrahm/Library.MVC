using System.Collections.Generic;

namespace Library.Domain
{
    public class BookDetails
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string Description { get; set; }
        public IList<Book> Copies { get; set; }
    }
}
