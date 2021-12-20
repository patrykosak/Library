﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Tag
    {
        [Key]
        public int TagID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int ISBN { get; set; }
        public virtual Book Book { get; set; }
    }
}