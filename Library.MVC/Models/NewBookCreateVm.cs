using Library.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class NewBookCreateVm
    {
        [Required]
        public string ISBN { get; set; }
        [Display(Name = "Titel")]
        [MaxLength(25)]
        public string Title { get; set; }
        [Display(Name = "Författare")]
        public SelectList AuthorList { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
