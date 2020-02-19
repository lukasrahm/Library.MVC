using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class BookAddCopiesVm
    {
        [Display(Name = "Bokdetaljer")]
        public SelectList BookList { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int AmountOfCopies { get; set; }
    }
}
