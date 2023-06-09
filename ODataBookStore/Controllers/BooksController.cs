using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ODataBookStore.Models;

namespace ODataBookStore.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class BooksController : ODataController
    {
        private BookStoreContext db;

        public BooksController(BookStoreContext context)
        {
            this.db = context;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            if (context.Books.Count() == 0)
            {
                foreach (var b in DataSource.GetBooks())
                {
                    context.Books.Add(b);
                    context.Presses.Add(b.Press);
                }
                context.SaveChanges();
            }
        }

        [HttpGet]
        [EnableQuery(PageSize = 5)]
        public IActionResult Get()
        {
            return Ok(db.Books);
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get(int key, string version)
        {
            return Ok(db.Books.FirstOrDefault(c => c.Id == key));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return Created(book);
        }

		[HttpPut("{key}")]
        public IActionResult Put([FromRoute] int key, [FromBody] Book book)
        {
			var b = db.Books.FirstOrDefault(c => c.Id == key);

            if (b == null)
            {
                return NotFound();
            }
            b.Title = book.Title;
            b.ISBN = book.ISBN;
            b.Press = book.Press;
            b.Author = book.Author;
            b.Price = book.Price;
            b.Location = book.Location;
            db.Update(b);
			db.SaveChanges();
            return Created(book);
		}

		[HttpDelete("{key}")]
        public IActionResult Delete([FromRoute] int key)
        {
            var b = db.Books.FirstOrDefault(c => c.Id == key);
            if (b == null)
            {
                return NotFound();
            }
            db.Books.Remove(b);
            db.SaveChanges();
            return Ok();
        }
    }
}
