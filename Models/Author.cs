using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Book> WrittenBooks { get; set; }

        public string GetString()
        {
            return this.Name + " " + this.Surname;
        }
    }
}