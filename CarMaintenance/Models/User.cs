using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Id { get; set; }

        public Car Car { get; set; }
    }
}
