using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Book
    {
        [Key]
        public int ISBN { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [Required]
        [MaxLength(60)]
        public string Author { get; set; }
        [Required]
        [Range(1600,2021)]
        public int PublicYear { get; set; }
        public int Amount { get; set; }

    }
}