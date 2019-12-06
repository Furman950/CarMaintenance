using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Models
{
    public class Car
    {
        public string VIN { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }
    }
}
