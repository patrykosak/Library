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
            IdentityManager elo = new IdentityManager();


            return View("Cart");
        }
        public ActionResult CartBin()
        {

            List<CartItem> cart = (List<CartItem>)Session["cart"];

            if (Session["cartbin"] == null)
            {
                List<CartItem> cartbin = new List<CartItem>();
                foreach (CartItem c in cart)
                { cartbin.Add(c); }
                Session["cartbin"] = cartbin;

            }
            else
            {
                List<CartItem> cartbin = (List<CartItem>)Session["cartbin"];

            }

            return View("CartBin");

        }

        //public ActionResult ToPdf(int id, PdfCreator.InvoiceType invoiceType)
        //  {
        /* Invoice invoice = _invoiceRepository.GetById(id); // _invoiceRepository - prywatne repozytorium faktur
         string path = Server.MapPath("~/bin/lastInvoice.pdf"); // po stronie serwera, faktura zapisywana jest do pliku lastInvoice.pdf (być może nie jest to ostateczne rozwiązanie)
         string fileName = invoice.CreationDate.Value.ToShortDateString() + "_" + invoice.Customer.CompanyName + ".pdf"; // nazwa pliku otrzymywanego przez użytkownika jest postaci dataWystawienia_nazwaFirmyKlienckiej.pdf

         PdfCreator pdfCreator = new PdfCreator(invoice); // klasa PdfCreator tworzy dokument PDF na podstawie encji faktury z bazy danych
         pdfCreator.Create(invoiceType);
         pdfCreator.SaveDocument(path, false);
         */
        //  return File(path, "PDF|*.pdf", fileName); // zwrócenie pliku PDF

        //}


        private int isExisting(int id)
        {
            //db.Cart.Add((Cart)Session["cart"]);
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Book.ISBN == id)
                    return i;
            return -1;

        }
        private int isExisting2(int id)
        {
            //db.Cart.Add((Cart)Session["cart"]);
            List<CartItem> cart = (List<CartItem>)Session["cartbin"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Book.ISBN == id)
                    return i;
            return -1;

        }
        public ActionResult OrderNow([Optional] int id, [Optional] int quantity) //Add to cart
        {
            if (quantity == 0 || id == 0)
            {
                return View("Cart");
            }

            if (Session["cart"] == null)
            {

                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem(db.Books.Find(id), quantity));
                Session["cart"] = cart;

            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                    cart.Add(new CartItem(db.Books.Find(id), quantity));
                else
                    cart[index].Quantity += quantity;
                Session["cart"] = cart;
                Session["licz"] = cart.Count;
            }
            return View("Cart");
        }
        public ActionResult OrderNow2(int? id, int? amount) //Add to cart afyer removal
        {
            if (amount == null || id == null)
            {
                return View("Cart");
            }

            if (Session["cart"] == null)
            {

                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem(db.Books.Find(id), (int)amount));
                Session["cart"] = cart;

            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                int index = isExisting((int)id);
                if (index == -1)
                    cart.Add(new CartItem(db.Books.Find(id), (int)amount));
                else
                    cart[index].Quantity += (int)amount;
                Session["cart"] = cart;
            }
            List<CartItem> cart2 = new List<CartItem>();
            cart2 = (List<CartItem>)Session["cartbin"];
            int index2 = isExisting2((int)id);
            cart2.RemoveAt(index2);
            return View("Cart");
        }
        public ActionResult Remove(int id, int quantity) //remove from cart
        {
            int index = isExisting(id);
            List<CartItem> cart = (List<CartItem>)Session["cart"];

            if (Session["cartbin"] == null)
            {

                List<CartItem> cartbin = new List<CartItem>();
                cartbin.Add(new CartItem(db.Books.Find(id), quantity));
                Session["cartbin"] = cartbin;

            }
            else
            {
                List<CartItem> cartbin = (List<CartItem>)Session["cartbin"];
                int index2 = isExisting2(id);
                if (index2 == -1)
                    cartbin.Add(new CartItem(db.Books.Find(id), quantity));
                else
                    cartbin[index2].Quantity += quantity;
                Session["cartbin"] = cartbin;
            }
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("CartBin");
        }

        public async System.Threading.Tasks.Task<ActionResult> MakeAnOrder()
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];

            Cart koszyk = new Cart();
            foreach (CartItem item in cart)
            {
                var e = db.Books.Where(x => x.ISBN == item.ISBN).FirstOrDefault();
                if (e != null)
                {
                    e.Amount -= item.Quantity;
                    db.SaveChanges();
                }
            }

            //sending email
            String messageText = "";
            foreach (CartItem item in cart)
            {
                messageText += item.GetString();
            }
            var body = "<h2>Email From: {0} about rented products ({1})</h2></br><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            string userName = User.Identity.GetUserName();

            message.To.Add(new MailAddress(userName));
            message.From = new MailAddress("libraryemailsend@gmail.com");
            message.Subject = "Your email subject";
            message.Body = string.Format(body, "Library", "", messageText);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "libraryemailsend@gmail.com",
                    Password = "Starekt1!"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }

            System.Threading.Thread.Sleep(1000);


            foreach (CartItem item in cart)
            {
                item.DateCreated = DateTime.Now;
                //db.CartItems.Add(item);
            }
            koszyk.CartItems = cart;
            koszyk.userId = User.Identity.GetUserId();
            //db.Cart.Add(koszyk);
            //db.SaveChanges();
            Order zamowienie = new Order();

            zamowienie.Cart = koszyk;
            zamowienie.userId = User.Identity.GetUserId();
            zamowienie.status = "Nowe";

            db.Orders.Add(zamowienie);
            db.SaveChanges();


            return View("CartBin");
        }


        public ActionResult OrderAgain(int id2)
        {
            var itemki = db.Cart.Where(o => o.CartID == id2).FirstOrDefault();
            if (Session["cart"] == null)
            {

                List<CartItem> cart = new List<CartItem>();
                foreach (CartItem item in itemki.CartItems)
                {
                    cart.Add(item);
                }
                Session["cart"] = cart;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                foreach (var item in itemki.CartItems)
                {
                    int index = isExisting(item.ISBN);
                    if (index == -1)
                        cart.Add(new CartItem(db.Books.Find(item.ISBN), item.Quantity));
                    else
                        cart[index].Quantity += item.Quantity;
                }
                Session["cart"] = cart;
                Session["licz"] = cart.Count;
            }
            return View("Cart");
        }
    }
}