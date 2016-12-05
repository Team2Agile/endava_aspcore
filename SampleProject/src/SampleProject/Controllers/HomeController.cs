using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using SampleProject.Models;
using SampleProject.Repositories;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleProject.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ICarRepository carRepository;

        public HomeController(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        [Route("[action]")]
        // GET: /<controller>/
        public IActionResult Index([FromServices] IMemoryCache cache, [FromServices] IConfiguration Configuration)
        {
            ViewBag.Title = "Cars";
            IEnumerable<Car> carList;
            string cacheKey = Configuration["CacheSettings:CarsCacheKey"];
            ViewBag.Title = "My favorite cars";
            if (!cache.TryGetValue(cacheKey, out carList))
            {
                carList = carRepository.GetCars();
                if (carList != null)
                {
                    cache.Set(
                        cacheKey,
                        carList.ToList(),
                        new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10)));
                }
                else
                {
                    return NotFound();
                }
            }

            if (carList == null)
            {
                return NotFound();
            }

            return View(carList);
        }

        [Route("[action]")]
        public IActionResult Details([FromServices] IConfiguration Configuration, [FromServices] IMemoryCache cache, int id)
        {
            Car carDetails;
            string cacheKey = string.Format($"{Configuration["CacheSettings:CarCacheKey"]}{id}");

            if (!cache.TryGetValue(cacheKey, out carDetails))
            {
                carDetails = carRepository.GetCar(id);
                if (carDetails != null)
                {
                    cache.Set(
                        cacheKey,
                        carDetails,
                        new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10)));
                }
                else
                {
                    return NotFound();
                }
            }

            if (carDetails == null)
            {
                return NotFound();
            }
            return View(carDetails);
        }
    }
}
