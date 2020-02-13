using Library.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class LoansVm
    {
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public ICollection<Member> Members { get; set; } = new List<Member>();
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<BookDetails> Details { get; set; } = new List<BookDetails>();
    }
}
