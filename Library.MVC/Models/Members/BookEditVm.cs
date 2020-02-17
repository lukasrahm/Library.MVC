using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Library.MVC.Models
{
    public class MemberEditVm
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string SSN { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

    }
}