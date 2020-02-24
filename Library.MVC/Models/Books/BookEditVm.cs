using Library.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.MVC.Models
{
    public class BookEditVm
    { 
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(13)]
        public string ISBN { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Titel")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Författare")]
        public int AuthorId { get; set; }
        public SelectList Authors { get; set; }
        [Required]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Required]
        [Range(1,100)]
        [Display(Name = "Kopior")]
        public int AmountOfCopies { get; set; }
    }
}