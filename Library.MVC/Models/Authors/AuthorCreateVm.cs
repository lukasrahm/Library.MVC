﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class AuthorCreateVm
    {
        [Display(Name = "Namn")]
        [Required]
        public string Name { get; set; }
    }
}
