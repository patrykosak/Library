using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class CartItem
    {
        [Key]
        public int ItemID { get; set; }

        public int CartID { get; set; }

        public System.DateTime DateCreated { get; set; }

        public int ISBN { get; set; }

        public virtual Book Book { get; set; }

        public int Quantity { get; set; }

        public CartItem(int ISBN, int quantity)
        {

            this.ISBN = ISBN;
            this.Quantity = quantity;
        }
        public CartItem()
        { }
        public string GetString()
        {
            return Environment.NewLine + "Book: " + Book.Title + ", Quantity: " + this.Quantity;
        }
    }
}