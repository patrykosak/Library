using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public enum Status
    {
        [Display(Name = "Nowe zamówienie")]
        newOrder,
        [Display(Name = "Odebrana")]
        received,
        [Display(Name = "Zwrócona")]
        returned
    }
}