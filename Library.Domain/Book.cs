using System.Collections.Generic;

namespace Library.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public int DetailsId { get; set; }
        public BookDetails Details { get; set; }

        public int? LoanId { get; set; }
        public Loan Loan { get; set; }
    }
}