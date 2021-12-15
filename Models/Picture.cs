using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Picture
    {
        [Key]
        public int PictureID { get; set; }
        [Display(Name = "Link")]
        public string Link { get; set; }
        public int ISBN { get; set; }
        public virtual Book Book { get; set; }
    }
}