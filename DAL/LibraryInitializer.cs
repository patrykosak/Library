using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.DAL
{
    public class LibraryInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<LibraryContext>
    {
        protected override void Seed(LibraryContext context)
        {
            var books = new List<Book>
            {
            new Book{ISBN=1, Title="Pakt Piłsudski-Lenin",Author="Piotr Zychowicz",PublicYear=2014,Amount=4},
            new Book{ISBN=2, Title="Pakt Ribbentrop-Beck",Author="Piotr Zychowicz",PublicYear=2014,Amount=3}
            };

            books.ForEach(s => context.Books.Add(s));
            context.SaveChanges();
            var authors = new List<Author>
            {
            new Author{ID=1,Name="Piotr",Surname="Zychowicz", BirthDate=DateTime.Parse("1970-09-01")}
 
            };
            authors.ForEach(s => context.Authors.Add(s));
            context.SaveChanges();
            var publishingHouses = new List<PublishingHouse>
            {
            new PublishingHouse{Id=1,Name="Wydawnictwo"},
            };
            publishingHouses.ForEach(s => context.PublishingHouses.Add(s));
            context.SaveChanges();
        }
    }
}