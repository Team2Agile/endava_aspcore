using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using SampleProject.Data;

using Microsoft.Extensions.Caching.Memory;
using SampleProject.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleProject.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        public ApplicationDbContext DbContext { get; set; }

        public HomeController(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index([FromServices] IMemoryCache cache)
        {
            var carList = new List<Car>();

            string cacheKey = "myCars";
            if (!cache.TryGetValue(cacheKey, out carList))
            {
                carList = DbContext.Cars.ToList();
                cache.Set(cacheKey, carList,
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10)));
            }
            return View(carList);
        }

        [HttpPost]
        public IActionResult Details()
        {
            return Ok();
        }
    }
}
