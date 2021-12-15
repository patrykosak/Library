using Library.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {

            return View("Cart");
        }

        private int isExisting(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ISBN == id)
                    return i;
            return -1;

        }

        public ActionResult OrderNow([Optional] int id, [Optional] string title, [Optional] int quantity) //Add to cart
        {
            if (quantity == 0 || id == 0)
            {
                return View("Cart");
            }

            if (Session["cart"] == null)
            {

                List<CartItem> cart = new List<CartItem>();
                List<String> titles = new List<String>();
                //cart.Add(new CartItem(db.Books.AsNoTracking().FirstOrDefault(b => b.ISBN == id), 1));
                cart.Add(new CartItem(id, 1));
                titles.Add(title);
                Session["cart"] = cart;
                Session["titles"] = titles;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                List<String> titles = (List<String>)Session["titles"];
                int index = isExisting(id);
                if (index == -1)
                {
                    //cart.Add(new CartItem(db.Books.AsNoTracking().FirstOrDefault(b => b.ISBN == id), 1));
                    cart.Add(new CartItem(id, 1));
                    titles.Add(title);
                }
                else
                {
                    Session["cart"] = cart;
                    Session["titles"] = titles;
                }
                    Session["licz"] = cart.Count;
            }
            return View("Cart");
        }
 
        public ActionResult Remove(int id, int quantity) //remove from cart
        {
            int index = isExisting(id);
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            List<String> titles = (List<String>)Session["titles"];

            if (index != -1) {
                cart.RemoveAt(index);
                titles.RemoveAt(index);
                Session["cart"] = cart;
                Session["titles"] = titles;
            }
            return View("Cart");
        }

        public async System.Threading.Tasks.Task<ActionResult> MakeAnOrder()
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];

            Cart koszyk = new Cart();
            foreach (CartItem item in cart)
            {
                var e = db.Books.AsNoTracking().FirstOrDefault(x => x.ISBN == item.ISBN);
                if (e != null)
                {
                    e.Amount = 2;
                }
            }

            //sending email
            //String messageText = "";
            //foreach (CartItem item in cart)
            //{
            //    messageText += item.GetString();
            //}
            //var body = "<h2>Email From: {0} about rented products ({1})</h2></br><p>Message:</p><p>{2}</p>";
            //var message = new MailMessage();
            //string userName = User.Identity.GetUserName();

            //message.To.Add(new MailAddress(userName));
            //message.From = new MailAddress("libraryemailsend@gmail.com");
            //message.Subject = "Your email subject";
            //message.Body = string.Format(body, "Library", "", messageText);
            //message.IsBodyHtml = true;

            //using (var smtp = new SmtpClient())
            //{
            //    var credential = new NetworkCredential
            //    {
            //        UserName = "libraryemailsend@gmail.com",
            //        Password = "Starekt1!"
            //    };
            //    smtp.Credentials = credential;
            //    smtp.Host = "smtp.gmail.com";
            //    smtp.Port = 587;
            //    smtp.EnableSsl = true;
            //    await smtp.SendMailAsync(message);
            //}

            //System.Threading.Thread.Sleep(1000);


            foreach (CartItem item in cart)
            {
                item.DateCreated = DateTime.Now;
                //db.CartItems.Add(item);
            }
            koszyk.CartItems = cart;
            String userID = User.Identity.GetUserId();
            koszyk.userId = userID;
            //db.Cart.Add(koszyk);
            Order zamowienie = new Order();

            zamowienie.Cart = koszyk;
            zamowienie.userId = userID;
            zamowienie.status = Status.newOrder;


            db.Orders.Add(zamowienie);
            db.SaveChanges();
            Session["cart"] = null;
            Session["titles"] = null;
            Session["licz"] = 0;

            return View("Cart");
        }

    }
}