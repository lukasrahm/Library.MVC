using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC.Models
{
    public class MemberCreateVm
    {
        [Display(Name = "SSN")]
        [MinLength(12)]
        [MaxLength(12)]
        [Required]
        public string SSN { get; set; }


        [Display(Name = "Namn")]
        public string Name { get; set; }

    }
}
