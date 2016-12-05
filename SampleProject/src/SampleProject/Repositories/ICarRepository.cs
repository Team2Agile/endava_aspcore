using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleProject.Models;

namespace SampleProject.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars();

        Car GetCar(int id);
    }
}
