using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Issue
    { 
        [Key]
        public int IssueID { get; set; }
        public int ISBN { get; set; }
        public virtual Book Book { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime RaturnDate { get; set; }
    }
}