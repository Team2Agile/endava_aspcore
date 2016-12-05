using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using SampleProject.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleProject.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public ApplicationDbContext DbContext { get; set; }

        private IMemoryCache cache;

        public HomeController(ApplicationDbContext dbContext, [FromServices] IMemoryCache cache)
        {
            this.DbContext = dbContext;
            this.cache = cache;
        }

        [Route("[action]")]
        // GET: /<controller>/
        public IActionResult Index([FromServices] IConfiguration Configuration)
        {
            ViewBag.Title = "Cars";
            List<Car> carList = new List<Car>();
            string cacheKey = Configuration["CacheSettings:CarsCacheKey"];
            if (!cache.TryGetValue(cacheKey, out carList))
            {
                ViewBag.Title = "My favorite cars";
                carList = DbContext.Cars.ToList();
                cache.Set(
                    cacheKey,
                    carList,
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10)));

            }
            return View(carList);
        }

        [Route("[action]")]
        public IActionResult Details(int id)
        {
            Car carDetails = DbContext.Cars.FirstOrDefault(x => x.Id == id) ?? Car.NoCar();
            return View(carDetails);
        }
    }
}
