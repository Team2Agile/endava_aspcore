﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SampleProject.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleProject.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Route("[action]")]
        // GET: /<controller>/
        public IActionResult Index([FromServices] IMemoryCache cache)
        {
            DreamCars dc;
            string cacheKey = "fullCarList";
            if (cache.TryGetValue(cacheKey, out dc))
            {
                ViewBag.Title = "My favorite cars";
                dc = new DreamCars();
                dc.MyDreamCars = new List<Car>();
                dc.MyDreamCars.Add(new Car("VW", "Golf"));
                dc.MyDreamCars.Add(new Car("Skoda", "Octavia"));
                dc.MyDreamCars.Add(new Car("BMW", "5 Series"));
                dc.MyDreamCars.Add(new Car("Dacia", "Sandero"));

                cache.Set(
                    cacheKey,
                    dc,
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10)));

            }
            return View(dc);
        }

        [HttpPost]
        public IActionResult Details()
        {
            return Accepted();
        }
    }
}
