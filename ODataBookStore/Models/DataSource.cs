using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ODataBookStore.Models
{
    public static class DataSource
    {
        private static IList<Book> listBooks { get; set; }

        public static IList<Book> GetBooks()
        {
            if (listBooks != null)
            {
                return listBooks;
            }

            listBooks = new List<Book>();
            Book book = new Book
            {
                Id = 1,
                ISBN = "987-0-321-87758-1",
                Title = "Essentila C# 5.0",
                Author = "Mark Michaelis",
                Location = new Address
                {
                    City = "Ha Noi",
                    Street = "Ho Guom"
                },
                Press = new Press
                {
                    Id = 1,
                    Name = "Addison Wesley",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);
            book = new Book
            {
                Id = 2,
                ISBN = "987-0-321-87758-1",
                Title = "Master Git",
                Author = "Mark Michaelis",
                Location = new Address
                {
                    City = "Ha Noi",
                    Street = "Ho Guom"
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "NamHK3",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);
            return listBooks;
        }
    }
}
