using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class BookCreateVm
    {
        [Display(Name = "Bokdetaljer")]
        public SelectList DetailsList { get; set; }
        [Required]
        public int DetailId { get; set; }
        [Required]
        public int AmountOfCopies { get; set; }
    }
}
