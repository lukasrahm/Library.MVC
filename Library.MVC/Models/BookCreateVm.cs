﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class BookCreateVm
    {
        [Required]
        public string ISBN { get; set; }
        [Display(Name = "Titel")]
        [MaxLength(25)]
        public string Title { get; set; }
        [Display(Name = "Bok")]
        public SelectList BookDetailsList { get; set; }
        public int AmountOfCopies { get; set; }
    }
}
