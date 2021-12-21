using Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            new Author{AuthorID=1,Name="Piotr",Surname="Zychowicz", BirthDate=DateTime.Parse("1970-09-01")},
            new Author{AuthorID=2,Name="Szzymon",Surname="Nowak", BirthDate=DateTime.Parse("1977-09-01")}
            };
            authors.ForEach(s => context.Authors.Add(s));
            context.SaveChanges();

            var roles = new List<ApplicationRole>
            {
            new ApplicationRole{ Name = "Admin" },
            new ApplicationRole{ Name = "Worker" },
            new ApplicationRole{ Name = "User" },
        };
            roles.ForEach(s => context.Roles.Add(s));
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
            new Subcategory{SubcategoryID=2,Name="WW2",CategoryID=1},
            new Subcategory{SubcategoryID=3,Name="Football",CategoryID=2}
            };
            subcategories.ForEach(s => context.Subcategories.Add(s));
            context.SaveChanges();

            var books = new List<Book>
            {
            new Book{ISBN=1, Title="Pakt Piłsudski-Lenin",PublicYear=2014,Amount=4,SubcategoryID=2,AuthorID=1,publishingHouseID=1},
            new Book{ISBN=2, Title="Pakt Ribbentrop-Beck",PublicYear=2014,Amount=3,SubcategoryID=1,AuthorID=1,publishingHouseID=1,},
            new Book{ISBN=3, Title="Dziewczyny Wyklęte",PublicYear=2014,Amount=3,SubcategoryID=2,AuthorID=2,publishingHouseID=1,},
            new Book{ISBN=4, Title="Oddziały wyklętych",PublicYear=2014,Amount=4,SubcategoryID=2,AuthorID=2,publishingHouseID=1,}
            };

            books.ForEach(s => context.Books.Add(s));
            context.SaveChanges();


            var pictures = new List<Picture>
            {
            new Picture{PictureID=1, ISBN=1,Link="https://image.ceneostatic.pl/data/products/38600050/i-pakt-pilsudski-lenin-czyli-jak-polacy-uratowali-bolszewizm-i-zmarnowali-szanse-na-budowe-imperium.jpg"},
            new Picture{PictureID=2, ISBN=2,Link="https://ecsmedia.pl/c/pakt-ribbentrop-beck-b-iext85841818.jpg"},
            new Picture{PictureID=3, ISBN=3,Link="https://s.lubimyczytac.pl/upload/books/246000/246126/353522-352x500.jpg"},
            new Picture{PictureID=4, ISBN=4,Link="https://a.allegroimg.com/original/035ab8/c0f8b8ae4f9db7804a81fcec0394/Oddzialy-Wykletych-Szymon-Nowak"},
            };

            pictures.ForEach(s => context.Pictures.Add(s));
            context.SaveChanges();

            var files = new List<File>
            {
            new File{FileID=1, ISBN=1,Address="https://image.ceneostatic.pl/data/products/38600050/i-pakt-pilsudski-lenin-czyli-jak-polacy-uratowali-bolszewizm-i-zmarnowali-szanse-na-budowe-imperium.jpg"},
            };

            files.ForEach(s => context.Files.Add(s));
            context.SaveChanges();

            var tags = new List<Tag>
            {
            new Tag{TagID=1, ISBN=1, Name="History"},
            new Tag{TagID=1, ISBN=1, Name="World War II"},
            };

            tags.ForEach(s => context.Tags.Add(s));
            context.SaveChanges();

            var messages = new List<Message>
            {
            new Message{MessageID=1, Title="New Hours", Text="Library is now open 24/7"}
            };

            messages.ForEach(s => context.Messages.Add(s));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}