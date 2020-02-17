using Library.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class LoanMakeVm
    {
        [Required]
        public int MemberId { get; set; }
        [Display(Name = "Medlemmar")]
        public SelectList Members { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        public BookDetails Details { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateOfLoan { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateOfReturn { get; set; }
    }
}
