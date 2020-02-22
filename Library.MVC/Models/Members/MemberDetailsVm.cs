using Library.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class MemberDetailsVm
    {
        public int Id { get; set; }
        public string SSN { get; set; }
        public string Name { get; set; }
        public IList<Loan> Loans { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();

        public int Fees { get; set; }

        public int Payment { get; set; }
    }
}
