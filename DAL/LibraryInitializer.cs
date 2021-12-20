using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.DAL
{
    public class LibraryInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var authors = new List<Author>
            {
            new Author{AuthorID=1,Name="Piotr",Surname="Zychowicz", BirthDate=DateTime.Parse("1970-09-01")}

            };
            authors.ForEach(s => context.Authors.Add(s));
            context.SaveChanges();

            var publishingHouses = new List<PublishingHouse>
            {
            new PublishingHouse{publishingHouseID=1,Name="Rebis"},
            };
            publishingHouses.ForEach(s => context.PublishingHouses.Add(s));
            context.SaveChanges();
            var categories = new List<Category>
            {
            new Category{CategoryID=1,Name="History"},
            new Category{CategoryID=2,Name="Sport"}

            };
            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

            var subcategories = new List<Subcategory>
            {
            new Subcategory{SubcategoryID=1,Name="WW1",CategoryID=1},
            new Subcategory{SubcategoryID=2,Name="WW2",CategoryID=1}

            };
            subcategories.ForEach(s => context.Subcategories.Add(s));
            context.SaveChanges();

            var books = new List<Book>
            {
            new Book{ISBN=1, Title="Pakt Piłsudski-Lenin",PublicYear=2014,Amount=4,SubcategoryID=2,AuthorID=1,publishingHouseID=1},
            new Book{ISBN=2, Title="Pakt Ribbentrop-Beck",PublicYear=2014,Amount=3,SubcategoryID=1,AuthorID=1,publishingHouseID=1,}
            };

            books.ForEach(s => context.Books.Add(s));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}