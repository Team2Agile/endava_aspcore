using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Models
{
    public class Car
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Price { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
