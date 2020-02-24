using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Library.MVC.Models
{
    public class MemberEditVm
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(12)]
        [Display(Name = "Personnummer")]
        public string SSN { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Namn")]
        public string Name { get; set; }

    }
}