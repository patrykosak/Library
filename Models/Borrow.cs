using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Borrow
    {
        [Key]
        public int BorrowID { get; set; }
        [Required]
        public int ISBN { get; set; }
        public virtual Book Book { get; set; }
        [Required]
        public int UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BorrowDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RaturnDate { get; set; }
    }
}