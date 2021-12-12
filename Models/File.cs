using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class File
    {
        public int FileID { get; set; }
        [Display(Name = "Link")]
        public string Address { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        public int ISBN { get; set; }
        public virtual Book Book { get; set; }
    }
}