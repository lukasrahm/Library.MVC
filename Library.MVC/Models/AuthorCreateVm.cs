using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class AuthorCreateVm
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        public SelectList Books { get; set; }
    }
}
