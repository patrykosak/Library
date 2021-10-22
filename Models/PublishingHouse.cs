using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class PublishingHouse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        public virtual ICollection<Book> PublishedBooks { get; set; }
    }
}