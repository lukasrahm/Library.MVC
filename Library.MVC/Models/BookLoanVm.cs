using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class BookLoanVm
    {
        [Display(Name = "Medlems SSN")]
        [Required]
        [MinLength(12)]
        [MaxLength(12)]
        public string MemberSSN { get; set; }
        [Required]
        public int BookId { get; set; }
    }
}
