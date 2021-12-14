using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

        public string userId { get; set; }

        public virtual ApplicationUser user { get; set; }

    }
}