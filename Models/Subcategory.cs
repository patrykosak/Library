using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Subcategory
    {
        [Key]
        public int SubcategoryID { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}