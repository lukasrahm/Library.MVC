using System.Collections.Generic;

namespace Library.Domain
{
    public class Loan
    {
        public int Id { get; set; }
        public int DetailsId { get; set; }
        public BookDetails Details { get; set; }
    }
}