using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain
{
    public class Loan
    {
        public int Id { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime DateOfLoan { get; set; }
        public DateTime DateOfReturn { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}