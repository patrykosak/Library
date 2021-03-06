using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Range(1600,2021)]
        public int PublicYear { get; set; }
        public int Amount { get; set; }
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
        public int SubcategoryID { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        public int publishingHouseID { get; set; }
        public virtual PublishingHouse PublishingHouse { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}