using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleProject.Data;
using SampleProject.Models;

namespace SampleProject.Repositories
{
    public class CarRepository : ICarRepository
    {
        public ApplicationDbContext DbContext { get; set; }
        public IConfiguration Configuration { get; set; }

        public CarRepository(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public IEnumerable<Car> GetCars()
        {
            return DbContext.Cars;
        }

        public Car GetCar(int id)
        {
            return DbContext.Cars.FirstOrDefault(x => x.Id == id);
        }
    }
}
