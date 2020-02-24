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

        [Display(Name = "Personnummer")]
        public string SSN { get; set; }
        [Display(Name = "Namn")]
        public string Name { get; set; }
        public IList<Loan> Loans { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();

        [Display(Name = "Avgifter")]
        public int Fees { get; set; }

        public int Payment { get; set; }
    }
}
