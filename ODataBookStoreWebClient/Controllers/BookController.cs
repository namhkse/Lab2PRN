using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ODataBookStoreWebClient.Models;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Security.Permissions;
using System.Text;

namespace ODataBookStoreWebClient.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public BookController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7255/odata/Books";
        }

        public async Task<IActionResult> Index()
        {
            var response = await client.GetAsync(ProductApiUrl);
            string data = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(data);
            var ls = temp.value;
            List<Book> items = ((JArray)temp.value).Select(x => new Book
            {
                Id = (int)x["Id"],
                ISBN = (string)x["ISBN"],
                Title = (string)x["Title"],
            }).ToList();

            return View(items);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var response = await client.GetAsync($"{ProductApiUrl}?$filter=Id eq {id}");
			string data = await response.Content.ReadAsStringAsync();
			dynamic temp = JObject.Parse(data);
			var ls = temp.value;
            Book item = null;
            try
            {
                item = ((JArray)temp.value).Select(x => new Book
                {
                    Id = (int)x["Id"],
                    ISBN = (string)x["ISBN"],
                    Title = (string)x["Title"],
                    Author = (string)x["Author"],
                    Price = (decimal)x["Price"]
                }).ToList()[0];
            } catch (Exception e)
            {
                return NotFound();
            }

			return View(item);
		}


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            var json = JsonConvert.SerializeObject(book);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(ProductApiUrl, data);
            var result = await response.Content.ReadAsStringAsync();
            return Redirect("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
			var response = await client.DeleteAsync($"{ProductApiUrl}/{id}");
            return Redirect("/Book/Index");
		}
	}
}
