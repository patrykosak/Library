﻿using System;
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
        public int BookID { get; set; }
        public virtual Book Book { get; set; }
        [Required]
        public int UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        [Required]
        public DateTime RaturnDate { get; set; }
    }
}