using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataBookStore.Models;

namespace ODataBookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PressesController : ODataController
    {
        private BookStoreContext db;

        public PressesController(BookStoreContext context)
        {
            this.db = context;
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
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(db.Presses);
        }
    }
}
